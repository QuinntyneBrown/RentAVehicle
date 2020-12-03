using FluentValidation;

namespace RentAVehicle.Domain.Features.Clients
{
    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
