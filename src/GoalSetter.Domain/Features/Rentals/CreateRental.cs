using BuildingBlocks.Abstractions;
using GoalSetter.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSetter.Domain.Features.Rentals
{
    public class CreateRental
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Rental).NotNull();
                RuleFor(request => request.Rental).SetValidator(new RentalValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public RentalDto Rental { get; set; }
        }

        public class Response
        {
            public RentalDto Rental { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var rental = new Rental(
                    request.Rental.VehicleId, 
                    request.Rental.ClientId, 
                    request.Rental.DateRange, 
                    request.Rental.Total);

                _context.Store(rental);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Rental = rental.ToDto()
                };
            }
        }
    }
}
