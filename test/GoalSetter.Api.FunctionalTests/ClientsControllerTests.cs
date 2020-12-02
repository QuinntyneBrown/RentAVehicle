using GoalSetter.Testing;
using System.Threading.Tasks;
using Xunit;

namespace GoalSetter.Api.FunctionalTests
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

        }

        [Fact]
        public async Task Should_RemoveClient()
        {

        }
    }
}
