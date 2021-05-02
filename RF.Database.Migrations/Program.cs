using System;
using System.Linq;
using DbUp;
using RF.Infrastructure.Configurations.Extensions;

namespace RF.Database.Migrations
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var envName = ConfigurationsExtensionMethods.GetCurrentEnvironment();
            if (string.IsNullOrEmpty(envName))
            {
                throw new Exception("Environment not set");
            }
            
            
            var configuration = ConfigurationsExtensionMethods.BuildSharedConfiguration();
            var connectionString = ConfigurationsExtensionMethods.GetConnectionStringFromSharedConfigurations(configuration);

            if (string.IsNullOrEmpty(connectionString))
            {
                return ConnectionStringError();
            }

            var folderName = (envName == "Local") ? "Development" : envName;

            
            var scriptsPath = $"../../../SQLScripts/{folderName}";

            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(scriptsPath)
                    .LogToConsole()
                    .Build();

            if (!upgrader.IsUpgradeRequired())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No update is needed");
                return 0;
            }

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                Console.ReadLine();

                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            Console.ReadLine();
            return 1;
        }

        private static int ConnectionStringError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No connection string provided");
            return -1;
        }
    }
}