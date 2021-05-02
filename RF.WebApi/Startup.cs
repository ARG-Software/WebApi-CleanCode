using System;
using System.IO;
using System.Text;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RF.Infrastructure.Configurations.DependencyInjection.AutoFac.Modules;
using RF.Infrastructure.Configurations.Extensions;
using RF.Infrastructure.Configurations.Models;
using RF.Library.ActionFilters;

namespace RF.WebApi
{
    /// <summary>
    ///
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the hosting environment.
        /// </summary>
        /// <value>
        /// The hosting environment.
        /// </value>
        private IWebHostEnvironment HostingEnvironment { get; }

        /// <summary>
        /// Gets or sets the web services configurations, in this case the api configurations.
        /// </summary>
        /// <value>
        /// The web services configurations.
        /// </value>
        private WebServicesConfigurations WebServicesConfigurations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IWebHostEnvironment env)
        {
            Configuration = ConfigurationsExtensionMethods.BuildWebApiConfiguration(env);

            HostingEnvironment = env;
        }

        /// <summary>
        /// Configures the services.This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            WebServicesConfigurations = ConfigurationsExtensionMethods.GetApiConfigurations<WebServicesConfigurations>(Configuration);

            services.AddControllers(options => { options.Filters.Add(typeof(ValidateModelStateAttribute)); })
                  .AddControllersAsServices()
                  .AddNewtonsoftJson(

                     opt =>
                     {
                         opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                         opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                     }
                 )
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RoyaltyFlush Core API",
                    Version = "v1",
                    Description = "RoyaltyFlush ASP.NET Core Web API",
                });
                var xmlCommentsPath = AppDomain.CurrentDomain.BaseDirectory + @"RF.WebApi.xml";
                c.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowDevOriginsPolicy",
                    //builder => builder.AllowAnyOrigin()
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSwaggerDocument();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new WebServicesModule() { Configurations = WebServicesConfigurations });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseCors("AllowDevOriginsPolicy");

            app.UseSwagger((Action<SwaggerOptions>)null);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoyaltyFlush Core API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}