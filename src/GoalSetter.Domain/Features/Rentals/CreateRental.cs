using BuildingBlocks.Abstractions;
using FluentValidation;
using GoalSetter.Core.Models;
using GoalSetter.Core.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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

                var dateRange = DateRange.Create(request.Rental.Start, request.Rental.End);

                var vehicle = await _context.FindAsync<Vehicle>(request.Rental.VehicleId);

                if (vehicle.Deleted.HasValue)
                    throw new System.Exception("");

                var dailyRate = await _context.FindAsync<DailyRate>(vehicle.DailyRateId);

                var overlapingRentals = _context.Set<Rental>()
                    .Where(x => x.Cancelled == default && x.DateRange.Overlap(dateRange.Value) && x.VehicleId == request.Rental.VehicleId);

                if (overlapingRentals.Any())
                    throw new System.Exception();

                var rental = new Rental(
                    request.Rental.VehicleId, 
                    request.Rental.ClientId, 
                    dateRange.Value, 
                    (Price)(dailyRate.Price * dateRange.Value.Days));

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
