using BuildingBlocks.Abstractions;
using RentAVehicle.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentAVehicle.Domain.Features.Rentals
{
    public class GetRentals
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<RentalDto> Rentals { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Rentals = _context.Set<Rental>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
