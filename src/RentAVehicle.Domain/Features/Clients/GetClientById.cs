using BuildingBlocks.Abstractions;
using RentAVehicle.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentAVehicle.Domain.Features.Clients
{
    public class GetClientById
    {
        public class Request : IRequest<Response> {  
            public Guid ClientId { get; set; }        
        }

        public class Response
        {
            public ClientDto Client { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var client = await _context.FindAsync<Client>(request.ClientId);

                return new Response() { 
                    Client = client.ToDto()
                };
            }
        }
    }
}
