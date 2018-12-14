using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using UrlShortner.Data;
using UrlShortner.Models.Entities;
using UrlShortner.Services;

namespace UrlShortner
{
    public class Startup
    {   
        private IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.AddSingleton<IConfigService>(new ConfigService(Configuration));
            services.AddTransient<IUrlShortnerService, UrlShortnerService>();
            services.AddTransient<IGenericRepository<UrlRecord>, GenericRepository<UrlRecord>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
