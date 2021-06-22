using System;
using System.Threading.Tasks;
using BetCR.Caching.Impl;
using BetCR.Caching.Interface;
using BetCR.Repository.Repository;
using BetCR.Repository.Repository.Base;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Services;
using BetCR.Services.Base;
using BetCR.Services.External.Elenasport;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BetCR.Scheduled
{
    public class App
    {
        #region Public Properties

        public static IConfiguration Configuration { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task Run()
        {
            var environment = Environment.GetEnvironmentVariable("BetCR_Environment") ?? "Production";
            var host = WebHost.CreateDefaultBuilder()
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile(environment == "Production" ? "appsettings.json" : $"appsettings.{environment}.json", true, true);
                    builder.AddEnvironmentVariables("BetCR_");
                    Configuration = builder.Build();
                })
                .UseUrls("https://localhost:1234")
                .ConfigureServices(InitializeContainer)
                .Configure(builder => builder.UseHangfireDashboard("/hangfire"))
                .UseEnvironment(environment)
                .Build();

            await host.RunAsync();


        }

        #endregion Public Methods

        #region Private Methods

        private void InitializeContainer(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddLogging();
            services.AddDbContext<SQLiteDbContext>();
            services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
            services.AddScoped<IElenaFetcherService, ElenaFetcherService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMatchBetService, UserMatchBetService>();
            services.AddScoped<DbContext, SQLiteDbContext>();
            services.AddSingleton<ICache, InMemoryCache>();
            services.AddHangfire(configuration =>
            {
                configuration.UseSQLiteStorage("DefaultConnection");
            });
            services.AddHangfireServer(options =>
            {
                options.ServerName = "BetCR Hangfire";


            });

            JobStorage.Current = new SQLiteStorage("DefaultConnection");

            RecurringJob.AddOrUpdate<IUserMatchBetService>(s => s.CalculateUserPointsAsync(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate<IElenaFetcherService>(s => s.GetFixturesAsync(), Cron.HourInterval(12));
            RecurringJob.AddOrUpdate<IElenaFetcherService>(s => s.GetStandingsAsync(), Cron.HourInterval(12));
            RecurringJob.AddOrUpdate<IElenaFetcherService>(s => s.GetFixtureResultsAsync(), Cron.HourInterval(1));




        }

        #endregion Private Methods
    }
}