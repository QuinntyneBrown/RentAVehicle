using BuildingBlocks.Abstractions;
using GoalSetter.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSetter.Domain.Features.Vehicles
{
    public class GetVehicles
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<VehicleDto> Vehicles { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Vehicles = _context.Set<Vehicle>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
