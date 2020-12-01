using BuildingBlocks.Abstractions;
using BuildingBlocks.EventStore;
using GoalSetter.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Respawn;
using System;

namespace GoalSetter.Testing
{
    public class ApiTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        public IAppDbContext Context { get {

                var options = new DbContextOptionsBuilder()
                    .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GoalSetter;Integrated Security=SSPI;")
                    .Options;
                var context = new EventStoreDbContext(options);

                var eventStore = new EventStore(new MachineDateTime(), context);
                var aggregateSet = new AggregateSet(context, new MachineDateTime());

                return new AppDbContext(eventStore, aggregateSet);
            } 
        }

        private static Checkpoint checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                }
        };
        public ApiTestFixture()
        {
            checkpoint.Reset("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GoalSetter;Integrated Security=SSPI;").GetAwaiter().GetResult();
        }
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.UseEnvironment("Development");

        //    builder.ConfigureServices(services =>
        //    {
        //        services.AddEntityFrameworkInMemoryDatabase();

        //        var provider = services
        //            .AddEntityFrameworkInMemoryDatabase()
        //            .BuildServiceProvider();

        //        var serviceProvider = services.BuildServiceProvider();

        //        using (var scope = serviceProvider.CreateScope())
        //        {
        //            var scopedServices = scope.ServiceProvider;
                    
        //            Context = scopedServices.GetRequiredService<IAppDbContext>();

        //        }
        //    });
        //}

        protected override void Dispose(bool disposing)
        {
            checkpoint.Reset("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GoalSetter;Integrated Security=SSPI;").GetAwaiter().GetResult();

            base.Dispose(disposing);
        }

    }
}
