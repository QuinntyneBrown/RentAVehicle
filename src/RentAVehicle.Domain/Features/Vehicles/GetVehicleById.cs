using BuildingBlocks.Abstractions;
using RentAVehicle.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentAVehicle.Domain.Features.Vehicles
{
    public class GetVehicleById
    {
        public class Request : IRequest<Response> {  
            public Guid VehicleId { get; set; }        
        }

        public class Response
        {
            public VehicleDto Vehicle { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var vehicle = await _context.FindAsync<Vehicle>(request.VehicleId);

                var dailyRate = await _context.FindAsync<DailyRate>(vehicle.DailyRateId);

                return new Response() { 
                    Vehicle = vehicle.ToDto(dailyRate)
                };
            }
        }
    }
}
