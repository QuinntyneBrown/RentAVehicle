using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RentAVehicle.Core.ValueObjects
{
    public class ClientName : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public string Value { get; private set; }

        protected ClientName()
        {

        }

        private ClientName(string value)
        {
            Value = value;
        }

        public static Result<ClientName> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<ClientName>("ClientName should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<ClientName>("ClientName name is too long.");

            return Result.Success(new ClientName(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(ClientName clientName)
        {
            return clientName.Value;
        }

        public static explicit operator ClientName(string clientName)
        {
            return Create(clientName).Value;
        }
    }
}
