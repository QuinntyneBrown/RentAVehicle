using BuildingBlocks.Abstractions;
using RentAVehicle.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentAVehicle.Domain.Features.Vehicles
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

                var vehiclesQuery = from vehicle in _context.Set<Vehicle>()
                                    join dailyRate in _context.Set<DailyRate>() 
                                    on vehicle.DailyRateId equals dailyRate.DailyRateId
                                    where !dailyRate.Deleted.HasValue && !vehicle.Deleted.HasValue
                                    select new
                                    {
                                        Vehicle = vehicle,
                                        DailyRate = dailyRate
                                    };

			    return new Response() { 
                    Vehicles = vehiclesQuery.Select(x => x.Vehicle.ToDto(x.DailyRate)).ToList()
                };
            }
        }
    }
}
