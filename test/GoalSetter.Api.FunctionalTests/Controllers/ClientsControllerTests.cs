using GoalSetter.Core.Models;
using GoalSetter.Domain.Features.Clients;
using GoalSetter.Testing;
using GoalSetter.Testing.Builders;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoalSetter.Api.FunctionalTests.Controllers
{
    public class ClientsControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ClientsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_AddClient()
        {
            var client = new ClientDtoBuilder().Build();

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { client }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateClient().PostAsync("api/clients", stringContent);

            var response = JsonConvert.DeserializeObject<CreateClient.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = await _fixture.Context.FindAsync<Client>(response.Client.ClientId);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Should_RemoveClient()
        {   
            var client = _fixture.Context.Store(new ClientBuilder().Build());

            await _fixture.Context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateClient().DeleteAsync($"api/clients/{client.ClientId}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await _fixture.Context.FindAsync<Client>(client.ClientId);

            Assert.True(sut.Deleted.HasValue);
        }
    }
}
