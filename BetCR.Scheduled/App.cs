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
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire.Dashboard;
using Console = System.Console;

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
            var environment = Environment.GetEnvironmentVariable("BETCR_ENVIRONMENT") ?? "Production";
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
                    builder.AddEnvironmentVariables("BETCR_");
                    Configuration = builder.Build();
                })
                .UseUrls("http://+:5002")
                .ConfigureServices(InitializeContainer)
                .Configure(builder => builder.UseHangfireDashboard("/hangfire", new DashboardOptions() { Authorization = new List<IDashboardAuthorizationFilter>() { new DashboardNoAuthorizationFilter() } }))
                .UseEnvironment(environment)
                .Build();

            Environment.GetEnvironmentVariable("BETCR_ENVIRONMENT");
            Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));

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