using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using RentAVehicle.Core.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace RentAVehicle.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ProcessDbCommands(args, host);

            host.Run();
        }

        private static void ProcessDbCommands(string[] args, IHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var appDbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

                if (args.Contains("ci"))
                    args = new string[4] { "dropdb", "migratedb", "seeddb", "stop" };

                if (args.Contains("dropdb"))
                {
                    context.Database.EnsureDeleted();
                }

                if (args.Contains("migratedb"))
                {
                    context.Database.Migrate();
                }

                if (args.Contains("seeddb"))
                {
                    context.Database.EnsureCreated();
                    DbInitializer.Initialize(appDbContext);
                }

                if (args.Contains("secret"))
                {
                    Console.WriteLine(SecretGenerator.Generate());
                    Environment.Exit(0);
                }

                if (args.Contains("stop"))
                    Environment.Exit(0);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
