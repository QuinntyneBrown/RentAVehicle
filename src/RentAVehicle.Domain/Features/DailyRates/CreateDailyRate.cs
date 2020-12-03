using BuildingBlocks.Abstractions;
using RentAVehicle.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RentAVehicle.Core.ValueObjects;

namespace RentAVehicle.Domain.Features.DailyRates
{
    public class CreateDailyRate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DailyRate).NotNull();
                RuleFor(request => request.DailyRate).SetValidator(new DailyRateValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public DailyRateDto DailyRate { get; set; }
        }

        public class Response
        {
            public DailyRateDto DailyRate { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var dailyRate = new DailyRate((Price)request.DailyRate.Price);

                _context.Store(dailyRate);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    DailyRate = dailyRate.ToDto()
                };
            }
        }
    }
}
