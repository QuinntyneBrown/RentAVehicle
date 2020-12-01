using BuildingBlocks.Abstractions;
using GoalSetter.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSetter.Domain.Features.Rentals
{
    public class GetRentalById
    {
        public class Request : IRequest<Response> {  
            public Guid RentalId { get; set; }        
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

                var rental = await _context.FindAsync<Rental>(request.RentalId);

                return new Response() { 
                    Rental = rental.ToDto()
                };
            }
        }
    }
}
