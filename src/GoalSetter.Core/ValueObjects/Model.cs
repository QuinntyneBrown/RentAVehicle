using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace GoalSetter.Core.ValueObjects
{
    public class Model : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public string Value { get; private set; }

        protected Model()
        {

        }

        private Model(string value)
        {
            Value = value;
        }

        public static Result<Model> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<Model>("Model should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<Model>("Model name is too long.");

            return Result.Success(new Model(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Model model)
        {
            return model.Value;
        }

        public static explicit operator Model(string model)
        {
            return Create(model).Value;
        }
    }
}
