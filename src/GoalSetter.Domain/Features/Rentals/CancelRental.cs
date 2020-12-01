using BuildingBlocks.Abstractions;
using FluentValidation;
using GoalSetter.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var rental = await _context.FindAsync<Rental>(request.RentalId);

                rental.Cancel(_dateTime.UtcNow);

                _context.Store(rental);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
