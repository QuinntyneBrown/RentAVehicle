using FluentValidation;

namespace RentAVehicle.Domain.Features.Vehicles
{
    public class VehicleValidator : AbstractValidator<VehicleDto>
    {
        public VehicleValidator()
        {
            RuleFor(x => x.Year).NotEqual(default(int));            
            RuleFor(x => x.Make).NotNull().NotEmpty();
            RuleFor(x => x.Model).NotNull().NotEmpty();
            RuleFor(x => x.DailyRate).GreaterThan(0);
        }
    }
}
