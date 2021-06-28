using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using BetCR.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BetCR.Web
{
    public class Program
    {
        #region Public Methods

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>();


                });



        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();


            host.Run();
        }

        #endregion Public Methods
    }
}