using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RDolce.Classes;
using RDolce.DataProvider;
using RDolce.Interfaces;
using Hangfire;
using RDolce.Controllers;

namespace RDolce
{
    public class Startup
    {
        public IConfigurationRoot appSettingsJson = AppSettingsJson.GetAppSettings();

        public static string connectionString; //= "Server=DESKTOP-H8PU1EK\\SQLEXPRESS17;Database=RDolce;Trusted_Connection=True;MultipleActiveResultSets=true";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IReservationFieldsDataProvider, ReservationFieldsDataProvider>();
            services.AddTransient<IUnitFieldsDataProvider, UnitFieldsDataProvider>();
            services.AddTransient<IPropertyDataProvider, PropertyDataProvier>();

            services.AddSingleton<IConfiguration>(Configuration);


            connectionString = Microsoft
 .Extensions
 .Configuration
 .ConfigurationExtensions
 .GetConnectionString(this.Configuration, "LocalDBConnectionString");

            services.AddHangfire(config =>
                   config.UseSqlServerStorage(connectionString));

            services.AddMvc().AddControllersAsServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
              //  app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            //The following line is also optional, if you required to monitor your jobs.
            //Make sure you're adding required authentication 
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseHttpsRedirection();
            app.UseMvc();

          
        }
    }
}
