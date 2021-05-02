using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using RF.Infrastructure.Configurations.Models;
using Microsoft.AspNetCore.Hosting;

namespace RF.Infrastructure.Configurations.Extensions
{
    /// <summary>
    /// Web Api ConfigurationObject
    /// </summary>
    public static class ConfigurationsExtensionMethods
    {
        private static string RootName = "Environments";
        private static string DefaultEnvironment = "Development";
        private static string BackgroundSection = "HangFire";

        public static string GetCurrentEnvironment()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return envName;
        }

        /// <summary>
        /// Gets the common configurations for each environment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static T GetApiConfigurations<T>(IConfiguration configuration) where T : BaseConfigurations
        {
            var configurations = configuration.Get<T>();

            return configurations;
        }

        /// <summary>
        /// Gets the background job configurations.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static BackgroundJobConfigurations GetBackgroundJobConfigurations(IConfiguration configuration)
        {
            var configurations = configuration.GetSection(BackgroundSection).Get<BackgroundJobConfigurations>();

            return configurations;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static string GetConnectionStringFromSharedConfigurations(IConfiguration configuration)
        {
            var environment = GetCurrentEnvironment() ?? DefaultEnvironment;

            var connectionString = configuration.GetSection(RootName).GetSection(environment).Get<BaseConfigurations>().ConnectionString;

            return connectionString;
        }

        /// <summary>
        /// Gets the shared configuration.
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot BuildSharedConfiguration()
        {
            //var sharedFolderPath = ;

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("sharedSettings.json")
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }

        /// <summary>
        /// Builds the web API configuration.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="sharedFolder">The shared folder.</param>
        /// <returns></returns>
        public static IConfigurationRoot BuildWebApiConfiguration(IWebHostEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return configuration.Build();
        }
    }
}