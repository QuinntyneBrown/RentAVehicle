using GoalSetter.Core.Models;
using GoalSetter.Domain.Features.Vehicles;
using GoalSetter.Testing;
using GoalSetter.Testing.Builders;
using Newtonsoft.Json;
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

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<CreateVehicle.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var vehicles = _fixture.Context.Set<Vehicle>();

            Assert.Single(vehicles);
        }
    }
}
