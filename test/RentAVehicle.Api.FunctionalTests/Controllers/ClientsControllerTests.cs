using Newtonsoft.Json;
using RentAVehicle.Core.Models;
using RentAVehicle.Domain.Features.Clients;
using RentAVehicle.Testing;
using RentAVehicle.Testing.Builders;
using System;
using System.Threading.Tasks;
using Xunit;
using BuildingBlocks.Core;

namespace RentAVehicle.Api.FunctionalTests.Controllers
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

            var response = await _fixture.CreateClient().PostAsAsync<dynamic, CreateClient.Response>(Endpoints.Post.AddClient, new { client });

            var sut = await _fixture.Context.FindAsync<Client>(response.Client.ClientId);

            Assert.NotEqual(default, sut.ClientId);
        }

        [Fact]
        public async Task Should_RemoveClient()
        {   
            var client = _fixture.Context.Store(new ClientBuilder().Build());

            await _fixture.Context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateClient().DeleteAsync(Endpoints.Delete.ClientBy(client.ClientId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await _fixture.Context.FindAsync<Client>(client.ClientId);

            Assert.True(sut.Deleted.HasValue);
        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string AddClient = "api/clients";
            }

            public static class Delete
            {
                public static string ClientBy(Guid clientId)
                {
                    return $"api/clients/{clientId}";
                }
            }
        }
    }
}
