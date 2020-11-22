namespace ApexLoader
{
    using ApexLoader.RavenDB;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Configuration;
    using Microsoft.Extensions.Logging.EventLog;

    using System;
    using System.Threading.Tasks;

    internal class Program
    {
        private static ApexConfigs Configure(IConfiguration config)
        {
            var apexConfigs = new ApexConfigs
            {
                Apex1 =
                {
                    Name = config["Apex1:Name"],
                    Url = config["Apex1:Url"],
                    Port = Convert.ToInt32(config["Apex1:Port"]),
                    User = config["Apex1:User"],
                    Password = config["Apex1:Password"],
                    Active = Convert.ToBoolean(config["Apex1:Active"])
                },
                Apex2 =
                {
                    Name = config["Apex2:Name"],
                    Url = config["Apex2:Url"],
                    Port = Convert.ToInt32(config["Apex2:Port"]),
                    User = config["Apex2:User"],
                    Password = config["Apex2:Password"],
                    Active = Convert.ToBoolean(config["Apex2:Active"])
                },
                RavenDBConnectionString = config["RavenDBConnectionString"],
                RavenDBDatabaseName = config["RavenDBDatabaseName"],
                DownloadInterval = Convert.ToInt32(config["DownloadInterval"])
            };


            return apexConfigs;
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine("Welcome to ApexLoader");
            var host = Host.CreateDefaultBuilder(args)
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
                    services.AddTransient<IDbClient, RavenDBClient>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration);
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    }

                    logging.AddEventLog(new EventLogSettings()
                    {
                        SourceName = "ApexLoader"
                    });
                })
                .UseWindowsService();
            return host;
        }

        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        // await host.RunConsoleAsync().ConfigureAwait(false);
    }
}