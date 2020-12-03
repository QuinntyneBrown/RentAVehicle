using RentAVehicle.Core.Models;

namespace RentAVehicle.Testing.Builders
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
