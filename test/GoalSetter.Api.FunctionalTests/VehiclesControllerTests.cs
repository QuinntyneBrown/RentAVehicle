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

            Assert.NotNull(await _fixture.Context.FindAsync<Vehicle>(vehicle.VehicleId));
        }

        [Fact]
        public async Task Should_RemoveVehicle()
        {
            var dailyRate = new DailyRate((Price)1m);

            var vehicle = new Vehicle(2004, "Honda", "Accord", dailyRate.DailyRateId);

            _fixture.Context.Store(dailyRate);

            _fixture.Context.Store(vehicle);

            await _fixture.Context.SaveChangesAsync(default);

            Assert.Single(_fixture.Context.Set<Vehicle>().Where(x => x.Deleted == null));

            var httpResponseMessage = await _fixture.CreateClient().DeleteAsync($"api/vehicles/{vehicle.VehicleId}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<RemoveVehicle.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            _fixture.Context = null;

            var vehicles = _fixture.Context.Set<Vehicle>();

            Assert.Empty(_fixture.Context.Set<Vehicle>().Where(x => x.Deleted == default));
        }
    }
}
