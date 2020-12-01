using BuildingBlocks.Abstractions;
using GoalSetter.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace GoalSetter.Domain.Features.Rentals
{
    public class CancelRental
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid RentalId { get; set; }
        }

        public class Response
        {
            public RentalDto Rental { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var rental = await _context.FindAsync<Rental>(request.RentalId);

                rental.Cancel(DateTime.UtcNow);

                _context.Store(rental);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
