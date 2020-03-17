using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace CleanArchitecture.Api
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));  //Commented out for local logging; DO NOT CHECK IN OR PUBLISH

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()                                //Added for local logging; DO NOT CHECK IN OR PUBLISH
                .CreateLogger();

            try
            {
                //Log.Warning("\n\n****  Starting CleanArchitecture.Api Service.  ****\n");  //Commented out for local logging; DO NOT CHECK IN OR PUBLISH
                Log.Warning("\n\n****  Starting CleanArchitecture.Api Application Service. Environment: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + "  ****\n");

                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<AppDbContext>();
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
