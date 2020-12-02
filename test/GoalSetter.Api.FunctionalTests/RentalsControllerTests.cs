using GoalSetter.Core.Models;
using GoalSetter.Core.ValueObjects;
using GoalSetter.Domain.Features.Rentals;
using GoalSetter.Testing;
using GoalSetter.Testing.Builders;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoalSetter.Api.FunctionalTests
{
    public class RentalsControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public RentalsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_CreateRentalWithAvailablVehiclesForDateRange()
        {
            var dailyRate = new DailyRate((Price)1m);

            var vehicle = new VehicleBuilder(2004, "Honda", "Pilot", dailyRate).Build();

            var client = new ClientBuilder().Build();

            _fixture.Context.Store(vehicle);

            _fixture.Context.Store(client);

            _fixture.Context.Store(dailyRate);

            await _fixture.Context.SaveChangesAsync(default);

            var rental = new RentalDto
            {
                ClientId = client.ClientId,
                VehicleId = vehicle.VehicleId,
                Start = DateTime.UtcNow.AddDays(1),
                End = DateTime.UtcNow.AddDays(2)
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { rental }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateClient().PostAsync("api/rentals", stringContent);

            var response = JsonConvert.DeserializeObject<CreateRental.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = await _fixture.Context.FindAsync<Rental>(response.Rental.RentalId);

            Assert.Equal((Price)1m, sut.Total);
        }

        [Fact]
        public async Task ShouldCancelRental()
        {
            var dailyRate = new DailyRate((Price)1m);

            var vehicle = new VehicleBuilder(2004, "Honda", "Pilot", dailyRate).Build();

            var client = new ClientBuilder().Build();

            var rental = new Rental(vehicle.VehicleId,client.ClientId,DateRange.Create(DateTime.UtcNow, DateTime.UtcNow.AddDays(1)).Value,(Price)100m);

            _fixture.Context.Store(vehicle);

            _fixture.Context.Store(client);

            _fixture.Context.Store(dailyRate);

            _fixture.Context.Store(rental);

            await _fixture.Context.SaveChangesAsync(default);

            _ = await _fixture.CreateClient().PutAsync($"api/rentals/{rental.RentalId}/cancel", default);

            var sut = await _fixture.Context.FindAsync<Rental>(rental.RentalId);

            Assert.NotEqual(default, sut.Cancelled);

        }

        [Fact]
        public async Task RemovedVehicleShouldNotBeAbleForNewRentals()
        {

        }

        [Fact]
        public async Task ShouldCalculateRentalPrice()
        {

        }
    }
}
