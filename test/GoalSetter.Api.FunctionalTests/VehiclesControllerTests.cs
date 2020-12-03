using GoalSetter.Core.Models;
using GoalSetter.Core.ValueObjects;
using GoalSetter.Domain.Features.Vehicles;
using GoalSetter.Testing;
using GoalSetter.Testing.Builders;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoalSetter.Api.FunctionalTests
{
    public class VehiclesControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public VehiclesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_CreateVehicle()
        {
            var vehicle = new VehicleDtoBuilder().Build();

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { vehicle }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateClient().PostAsync("api/vehicles", stringContent);

            var response = JsonConvert.DeserializeObject<CreateVehicle.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = await _fixture.Context.FindAsync<Vehicle>(response.Vehicle.VehicleId);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Should_RemoveVehicle()
        {
            var dailyRate = _fixture.Context.Store(new DailyRate((Price)1m));

            var vehicle = _fixture.Context.Store(new Vehicle(2004, "Honda", "Accord", dailyRate.DailyRateId));

            await _fixture.Context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateClient().DeleteAsync($"api/vehicles/{vehicle.VehicleId}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await _fixture.Context.FindAsync<Vehicle>(vehicle.VehicleId);

            Assert.True(sut.Deleted.HasValue);
        }
    }
}
