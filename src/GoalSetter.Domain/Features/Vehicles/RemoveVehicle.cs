using BuildingBlocks.Abstractions;
using GoalSetter.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace GoalSetter.Domain.Features.Vehicles
{
    public class RemoveVehicle
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid VehicleId { get; set; }
        }

        public class Response
        {
            public VehicleDto Vehicle { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var vehicle = await _context.FindAsync<Vehicle>(request.VehicleId);

                vehicle.Remove();

                _context.Store(vehicle);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
