using RentAVehicle.Core.Models;
using RentAVehicle.Domain.Features.Clients;

namespace RentAVehicle.Domain.Features
{
    public static class ClientExtensions
    {
        public static ClientDto ToDto(this Client client)
        {
            return new ClientDto
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Email = client.Email
            };
        }
    }
}
