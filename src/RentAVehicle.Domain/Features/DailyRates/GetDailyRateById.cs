using BuildingBlocks.Abstractions;
using RentAVehicle.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentAVehicle.Domain.Features.DailyRates
{
    public class GetDailyRateById
    {
        public class Request : IRequest<Response> {  
            public Guid DailyRateId { get; set; }        
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

                var dailyRate = await _context.FindAsync<DailyRate>(request.DailyRateId);

                return new Response() { 
                    DailyRate = dailyRate.ToDto()
                };
            }
        }
    }
}
