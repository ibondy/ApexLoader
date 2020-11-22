#region Copyright

// MIT License
// 
// Copyright (c) 2020 Ivan Bondy
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

namespace ApexLoader
{
    #region using

    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.EventLog;
    using RavenDB;

    #endregion

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
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
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

                                                 logging.AddEventLog(new EventLogSettings
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