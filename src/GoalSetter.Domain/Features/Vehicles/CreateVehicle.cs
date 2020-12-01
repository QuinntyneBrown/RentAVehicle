using BuildingBlocks.Abstractions;
using GoalSetter.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSetter.Domain.Features.Vehicles
{
    public class CreateVehicle
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Vehicle).NotNull();
                RuleFor(request => request.Vehicle).SetValidator(new VehicleValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public VehicleDto Vehicle { get; set; }
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

                var vehicle = new Vehicle(
                    request.Vehicle.Make,
                    request.Vehicle.Model,
                    request.Vehicle.DailyRateId
                    );

                _context.Store(vehicle);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Vehicle = vehicle.ToDto()
                };
            }
        }
    }
}
