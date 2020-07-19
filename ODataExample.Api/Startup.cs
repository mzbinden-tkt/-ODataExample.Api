using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using ODataExample.Api.Brokers;
using ODataExample.Api.Models;

namespace ODataExample.Api
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
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                //option.InputFormatters();
            }
            );
            // services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
            InitializeStorage(services);
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().Count().OrderBy();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
        private IEdmModel GetEdmModel()
        {
            var edmModelBuilder = new ODataConventionModelBuilder();
            edmModelBuilder.EntitySet<Customer>("Customers");
            return edmModelBuilder.GetEdmModel();
        }

        private void InitializeStorage(IServiceCollection services)
        {
            string connectingString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StorageBroker>(context => context.UseNpgsql(connectingString));
        }
    }
}
