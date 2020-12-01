using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoalSetter.Domain.Features.Rentals;
using System.Net;
using System.Threading.Tasks;

namespace GoalSetter.Api.Controllers
{
    [ApiController]
    [Route("api/rentals")]
    public class RentalsController
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateRentalRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateRental.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateRental.Response>> Upsert([FromBody] CreateRental.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpDelete("{rentalId}", Name = "CancelRentalRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]CancelRental.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("{rentalId}", Name = "GetRentalByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRentalById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetRentalById.Response>> GetById([FromRoute]GetRentalById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Rental == null)
            {
                return new NotFoundObjectResult(request.RentalId);
            }

            return response;
        }

        [Authorize]
        [HttpGet(Name = "GetRentalsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRentals.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRentals.Response>> Get()
            => await _mediator.Send(new GetRentals.Request());           
    }
}
