using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace GoalSetter.Core.ValueObjects
{
    public class Make : ValueObject
    {
        public const int MaxLength = 250;
        public string Value { get; }

        private Make(string value)
        {
            Value = value;
        }

        public static Result<Make> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<Make>("Make should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<Make>("Make name is too long.");

            return Result.Success(new Make(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Make make)
        {
            return make.Value;
        }

        public static explicit operator Make(string make)
        {
            return Create(make).Value;
        }
    }
}
