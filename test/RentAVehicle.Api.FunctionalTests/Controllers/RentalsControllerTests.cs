using RentAVehicle.Core.Models;
using RentAVehicle.Core.ValueObjects;
using RentAVehicle.Domain.Features.Rentals;
using RentAVehicle.Testing;
using RentAVehicle.Testing.Builders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RentAVehicle.Api.FunctionalTests.Controllers
{
    public class RentalsControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public RentalsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_CreateRentalWithAvailableVehiclesForDateRangeAndCalculatePrice()
        {
            var dailyRate = _fixture.Context.Store(new DailyRate((Price)1m));

            var vehicle = _fixture.Context.Store(new VehicleBuilder(2004, "Honda", "Pilot", dailyRate).Build());

            var client = _fixture.Context.Store(new ClientBuilder().Build());

            await _fixture.Context.SaveChangesAsync(default);

            var rental = new RentalDto
            {
                ClientId = client.ClientId,
                VehicleId = vehicle.VehicleId,
                Start = DateTime.UtcNow.AddDays(1),
                End = DateTime.UtcNow.AddDays(3)
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { rental }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateClient().PostAsync("api/rentals", stringContent);

            var response = JsonConvert.DeserializeObject<CreateRental.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = await _fixture.Context.FindAsync<Rental>(response.Rental.RentalId);

            Assert.Equal((Price)2m, sut.Total);
        }

        [Fact]
        public async Task ShouldCancelRental()
        {
            var dailyRate = _fixture.Context.Store(new DailyRate((Price)1m));

            var vehicle = _fixture.Context.Store(new VehicleBuilder(2004, "Honda", "Pilot", dailyRate).Build());

            var client = _fixture.Context.Store(new ClientBuilder().Build());

            var rental = _fixture.Context.Store(new Rental(vehicle.VehicleId,client.ClientId,DateRange.Create(DateTime.UtcNow, DateTime.UtcNow.AddDays(1)).Value,(Price)100m));

            await _fixture.Context.SaveChangesAsync(default);

            _ = await _fixture.CreateClient().PutAsync($"api/rentals/{rental.RentalId}/cancel", default);

            var sut = await _fixture.Context.FindAsync<Rental>(rental.RentalId);

            Assert.True(sut.Cancelled.HasValue);

        }

        [Fact]
        public async Task RemovedVehicleShouldNotBeAbleForNewRentals()
        {
            var dailyRate = _fixture.Context.Store(new DailyRate((Price)1m));

            var client = _fixture.Context.Store(new ClientBuilder().Build());
            
            var vehicle = _fixture.Context.Store(new VehicleBuilder(dailyRate: dailyRate).Remove().Build());

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

            var response = JsonConvert.DeserializeObject<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        [Fact]
        public async Task RentedVehiclesShouldNotBeAvailableForRent()
        {
            var dailyRate = _fixture.Context.Store(new DailyRate((Price)1m));

            var client = _fixture.Context.Store(new ClientBuilder().Build());

            var vehicle = _fixture.Context.Store(new VehicleBuilder(2004, "Honda", "Pilot", dailyRate).Build());

            var dateRange = DateRange.Create(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30));

            _ = _fixture.Context.Store(new Rental(vehicle.VehicleId, client.ClientId, dateRange.Value, (Price)100m));

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

            var response = JsonConvert.DeserializeObject<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }
    }
}
