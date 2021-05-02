using System;
using System.Text;
using Autofac;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard.Dark.Core;
using Hangfire.PostgreSql;
using Hangfire.StackTrace;
using Hangfire.Tags;
using Hangfire.Tags.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RF.Infrastructure.Configurations.DependencyInjection.AutoFac.Modules;
using RF.Infrastructure.Configurations.Extensions;
using RF.Infrastructure.Configurations.Models;
using RF.Library.ActionFilters;
using RF.RoyaltiesManagerApi.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace RF.RoyaltiesManagerApi
{
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
        /// Gets or sets the background job configurations.
        /// </summary>
        /// <value>
        /// The background job configurations.
        /// </value>
        private BackgroundJobConfigurations BackgroundJobConfigurations { get; set; }

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
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            WebServicesConfigurations = ConfigurationsExtensionMethods.GetApiConfigurations<WebServicesConfigurations>(Configuration);
            BackgroundJobConfigurations = ConfigurationsExtensionMethods.GetBackgroundJobConfigurations(Configuration);

            services.AddControllers(options => { options.Filters.Add(typeof(ValidateModelStateAttribute)); })
                .AddControllersAsServices()
                .AddNewtonsoftJson(

                    opt =>
                    {
                        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    }
                )
                .AddJsonOptions(
                    opts =>
                    {
                        opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
                    })
                   .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHangfire(configuration =>
            {
                configuration.UseSerializerSettings(new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto
                });
                configuration.UseFilter(new AutomaticRetryAttribute { Attempts = 0 });
                configuration.UseFilter(new HangfireExtendJobSuccessRetainFilter());
                configuration.UsePostgreSqlStorage(WebServicesConfigurations.ConnectionString,
                    new PostgreSqlStorageOptions()
                    {
                        PrepareSchemaIfNecessary = true,
                        SchemaName = BackgroundJobConfigurations.DatabaseSchema
                    });
                configuration.UseConsole(new ConsoleOptions
                {
                    ExpireIn = TimeSpan.FromDays(2.0)
                });

                configuration.UseStackTrace();

                configuration.UseTagsWithPostgreSql(new TagsOptions()
                {
                    TagsListStyle = TagsListStyle.Dropdown,
                    Storage = new PostgreSqlTagsServiceStorage(new PostgreSqlStorageOptions()
                    {
                        PrepareSchemaIfNecessary = true,
                        SchemaName = BackgroundJobConfigurations.DatabaseTagsSchema
                    })
                });
                configuration.UseDarkDashboard();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Royalties Manager Core API",
                    Version = "v1",
                    Description = "Royalties Manager ASP.NET Core Web API",
                });
                c.DescribeAllParametersInCamelCase();
                var xmlCommentsPath = AppDomain.CurrentDomain.BaseDirectory + @"RF.RoyaltiesManagerApi.xml";
                c.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddSwaggerDocument();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowDevOriginsPolicy",
                    //builder => builder.AllowAnyOrigin()
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new WebServicesModule() { Configurations = WebServicesConfigurations });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
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

            var options = new BackgroundJobServerOptions
            {
                WorkerCount = BackgroundJobConfigurations.NumberOfWorkers,
                ServerName = BackgroundJobConfigurations.ServerName
            };

            app.UseHangfireServer(options);
            app.UseHangfireDashboardCustomOptions(new HangfireDashboardCustomOptions()
            {
                DashboardTitle = () => "Royalties Manager",
            });
            app.UseHangfireDashboard("/statements", new DashboardOptions()
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}