using BuildingBlocks.Abstractions;
using BuildingBlocks.EventStore;
using GoalSetter.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
                var dateTime = new MachineDateTime();
                var eventStore = new EventStore(context, dateTime);
                var aggregateSet = new AggregateSet(context, dateTime);

                return new AppDbContext(eventStore, aggregateSet);
            } 
        }

        private static readonly Checkpoint checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                }
        };

        protected override void Dispose(bool disposing)
        {
            checkpoint.Reset("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GoalSetter;Integrated Security=SSPI;").GetAwaiter().GetResult();

            base.Dispose(disposing);
        }

    }
}
