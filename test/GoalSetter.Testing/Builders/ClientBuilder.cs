using GoalSetter.Core.Models;

namespace GoalSetter.Testing.Builders
{
    public class ClientBuilder
    {
        private Client _client;

        public ClientBuilder()
        {
            _client = new Client("Test","test@test.com");
        }

        public Client Build()
        {
            return _client;
        }
    }
}
