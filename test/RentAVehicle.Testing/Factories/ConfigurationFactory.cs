using RentAVehicle.Api;
using Microsoft.Extensions.Configuration;

namespace RentAVehicle.Testing.Factories
{
    public class ConfigurationFactory
    {
        public static IConfiguration Create()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();

            return configurationBuilder.Build();
        }
    }
}
