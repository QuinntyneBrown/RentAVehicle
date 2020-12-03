using RentAVehicle.Core.Models;
using RentAVehicle.Core.ValueObjects;

namespace RentAVehicle.Testing.Builders
{
    public class ClientBuilder
    {
        private Client _client;

        public ClientBuilder()
        {
            _client = new Client((ClientName)"Test",(Email)"test@email.com");
        }

        public Client Build()
        {
            return _client;
        }
    }
}
