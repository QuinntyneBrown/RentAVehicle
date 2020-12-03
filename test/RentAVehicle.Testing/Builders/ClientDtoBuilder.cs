using RentAVehicle.Domain.Features.Clients;

namespace RentAVehicle.Testing.Builders
{
    public class ClientDtoBuilder
    {
        private ClientDto _clientDto;

        public ClientDtoBuilder()
        {
            _clientDto = new ClientDto()
            {
                Name = "test",
                Email = "test@test.com"
            };
        }

        public ClientDto Build()
        {
            return _clientDto;
        }
    }
}
