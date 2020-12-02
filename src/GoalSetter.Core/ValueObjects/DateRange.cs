using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoalSetter.Core.ValueObjects
{
    public class DateRange : ValueObject
    {
        [JsonProperty]
        public DateTime StartDate { get; }
        [JsonProperty]
        public DateTime EndDate { get; }
        public int Days => (EndDate.Date - StartDate.Date).Days;

        protected DateRange()
        {

        }

        private DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public static Result<DateRange> Create(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return Result.Failure<DateRange>("Start Date should be less than End Date");

            return Result.Success(new DateRange(startDate, endDate));
        }

        public bool Overlap(DateRange dateRange)
        {
            _ = dateRange ?? throw new ArgumentNullException(nameof(dateRange));

            return StartDate < dateRange.EndDate && dateRange.StartDate < EndDate;
        }
    }
}
