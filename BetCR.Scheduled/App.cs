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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetCR.Library.Tracking;
using BetCR.Library.Tracking.Infrastructure;
using BetCR.Services.External.APIFootball;
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
                .ConfigureServices(InitializeContainer)
                .Configure(builder =>
                    builder.UseHangfireDashboard("/hangfire", new DashboardOptions() {Authorization = new List<IDashboardAuthorizationFilter>() {new DashboardNoAuthorizationFilter()}}))
                .UseEnvironment(environment)
#if DEBUG
                .UseUrls("http://localhost:5004")
#endif
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
            services.AddScoped<ElenaFetcherService>();
            services.AddScoped<ApiFootballFetcher>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMatchBetService, UserMatchBetService>();
            services.AddScoped<DataContext, SQLiteDbContext>();
            services.AddSingleton<ICache, InMemoryCache>();

            services.AddSingleton(_ => PublisherFactory.GetPublisher("events"));
            services.AddSingleton<ISubscriber, Subscriber>();


            services.AddHangfire(configuration => { configuration.UseSQLiteStorage(Configuration.GetConnectionString("HangfireConnection")); });
            services.AddHangfireServer(options => { options.ServerName = "BetCR Hangfire"; });


            JobStorage.Current = new SQLiteStorage(Configuration.GetConnectionString("HangfireConnection"));

            RecurringJob.AddOrUpdate<IUserMatchBetService>(s => s.CalculateUserPointsAsync(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate<ApiFootballFetcher>(s => s.GetFixturesAsync(), Cron.HourInterval(12));
            RecurringJob.AddOrUpdate<ApiFootballFetcher>(s => s.GetStandingsAsync(), Cron.HourInterval(12));
            RecurringJob.AddOrUpdate<ApiFootballFetcher>(s => s.GetFixtureResultsAsync(), Cron.HourInterval(1));

            services.AddHostedService<TrackingService>();
        }

        #endregion Private Methods
    }
}