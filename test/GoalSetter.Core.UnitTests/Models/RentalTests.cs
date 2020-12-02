using BuildingBlocks.EventStore;
using GoalSetter.Core.Models;
using GoalSetter.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GoalSetter.Core.UnitTests.Models
{
    public class RentalTests
    {

        public RentalTests()
        {

        }

        [Fact]
        public async Task Should_DeserializeCorrectly()
        {
            var fakeId = Guid.NewGuid();

            var dateRange = DateRange.Create(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2)).Value;

            var rental = new Rental(fakeId, fakeId, dateRange, (Price)1m);

            var dateTime = new MachineDateTime();
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("UnitTest").Options;
            var eventStoreDbContext = new EventStoreDbContext(options);
            var eventStore = new EventStore(eventStoreDbContext, dateTime);
            var aggregateSet = new AggregateSet(eventStoreDbContext, dateTime);
            var appDbContext = new AppDbContext(eventStore, aggregateSet);

            appDbContext.Store(rental);

            await appDbContext.SaveChangesAsync(default);

            var sut = await appDbContext.FindAsync<Rental>(rental.RentalId);


            Assert.NotEqual(default, sut.DateRange.StartDate);

        }
    }
}
