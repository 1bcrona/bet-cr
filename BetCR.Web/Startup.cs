using System;
using System.IO;
using System.Reflection;
using BetCR.Caching.Impl;
using BetCR.Caching.Interface;
using BetCR.Repository.Repository;
using BetCR.Repository.Repository.Base;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Services;
using BetCR.Services.Base;
using BetCR.Services.External;
using BetCR.Services.External.Elenasport;
using BetCR.Web.Controllers.API.Validator;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BetCR.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddLogging();
            services.AddDbContext<SQLiteDbContext>();
            services.AddSingleton<ICache, InMemoryCache>();
            services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
            services.AddScoped<IElenaFetcherService, ElenaFetcherService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<DbContext, SQLiteDbContext>();

            services.AddSingleton<ICache, InMemoryCache>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/User/Login/");
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddMediatR(Assembly.GetAssembly(GetType()));
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>());
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

      
            app.UseHttpsRedirection();

            Console.WriteLine(env.EnvironmentName);
            Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
            var path = Path.Combine(env.ContentRootPath, "wwwroot");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/static"
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "id",
                    pattern: "{controller}/{id}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
