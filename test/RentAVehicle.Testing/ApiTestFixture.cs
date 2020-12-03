using BuildingBlocks.Abstractions;
using BuildingBlocks.EventStore;
using RentAVehicle.Api;
using RentAVehicle.Testing.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using System;

namespace RentAVehicle.Testing
{
    public class ApiTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        private IAppDbContext _context;
        private IConfiguration _configuration;
        public ApiTestFixture()
        {
            checkpoint = new Checkpoint()
            {
                DbAdapter = DbAdapter.SqlServer,
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                }
            };

            _configuration = ConfigurationFactory.Create();
        }
        public IAppDbContext Context { get {

                if (_context == null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"])
                        .Options;

                    var context = new EventStoreDbContext(options);
                    var dateTime = new MachineDateTime();
                    var eventStore = new EventStore(context, dateTime);
                    var aggregateSet = new AggregateSet(context, dateTime);

                    _context = new AppDbContext(eventStore, aggregateSet);
                }

                return _context;
            }
            set
            {
                _context = value;
            }
        }

        private Checkpoint checkpoint;

        protected override void Dispose(bool disposing)
        {
            checkpoint.Reset(_configuration["Data:DefaultConnection:ConnectionString"]).GetAwaiter().GetResult();

            base.Dispose(disposing);
        }

    }
}
