using BuildingBlocks.Abstractions;
using FluentValidation;
using MediatR;
using RentAVehicle.Core.Models;
using RentAVehicle.Core.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace RentAVehicle.Domain.Features.Clients
{
    public class CreateClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Client).NotNull();
                RuleFor(request => request.Client).SetValidator(new ClientValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public ClientDto Client { get; set; }
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
                
                var client = _context.Store(new Client((ClientName)request.Client.Name, (Email)request.Client.Email));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Client = client.ToDto()
                };
            }
        }
    }
}
