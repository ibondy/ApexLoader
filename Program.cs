using System;

namespace ApexLoader
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Hosting.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Configuration;
    using Microsoft.Extensions.Http;
    using System.Collections.Generic;
    using ApexLoader.Cosmos;

    class Program
    {
        public static async Task Main(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine("Welcome to ApexLoader");
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    hostContext.HostingEnvironment.EnvironmentName = environment;
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Program>();
                    }
                    else
                    {
                        builder.AddJsonFile("settings.json", true);
                    }

                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton(Configure(hostingContext.Configuration));
                    services.AddSingleton<CookieDelegateHandler>();
                    services.AddHostedService<LoaderService>();
                    services.AddHttpClient<ApexService>().AddHttpMessageHandler<CookieDelegateHandler>();
                    services.AddSingleton<CosmosClient>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration);
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                     logging.AddConsole();
                    }
                });

            await host.RunConsoleAsync().ConfigureAwait(false);
            
        }

        private static ApexConfigs Configure(IConfiguration config)
        {
            var apexConfigs = new ApexConfigs();
            apexConfigs.Apex1.Name = config["Apex1:Name"];
            apexConfigs.Apex1.Url = config["Apex1:Url"];
            apexConfigs.Apex1.Port = Convert.ToInt32( config["Apex1:Port"]);
            apexConfigs.Apex1.User = config["Apex1:User"];
            apexConfigs.Apex1.Password = config["Apex1:Password"];
            apexConfigs.Apex1.Active = Convert.ToBoolean(config["Apex1:Active"]);

            apexConfigs.Apex2.Name = config["Apex2:Name"];
            apexConfigs.Apex2.Url = config["Apex2:Url"];
            apexConfigs.Apex2.Port = Convert.ToInt32(config["Apex2:Port"]);
            apexConfigs.Apex2.User = config["Apex2:User"];
            apexConfigs.Apex2.Password = config["Apex2:Password"];
            apexConfigs.Apex2.Active = Convert.ToBoolean(config["Apex2:Active"]);


            return apexConfigs;
        }
    }
}
