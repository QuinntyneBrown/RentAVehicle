using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace RentAVehicle.Core.ValueObjects
{
    public class Email : ValueObject
    {
        public const int MaxLength = 150;
        [JsonProperty]
        public string Value { get; private set; }

        protected Email()
        {

        }
        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<Email>("Email should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<Email>("Email name is too long.");

            if (!Regex.IsMatch(value, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Email is invalid");

            return Result.Success(new Email(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static explicit operator Email(string email)
        {
            return Create(email).Value;
        }
    }
}
