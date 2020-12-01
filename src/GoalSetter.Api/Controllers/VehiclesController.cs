using GoalSetter.Domain.Features.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GoalSetter.Api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost(Name = "CreateVehicleRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateVehicle.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateVehicle.Response>> Create([FromBody] CreateVehicle.Request request)
            => await _mediator.Send(request);

        [HttpDelete("{vehicleId}", Name = "RemoveVehicleRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveVehicle.Request request)
            => await _mediator.Send(request);

        [HttpGet("{vehicleId}", Name = "GetVehicleByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVehicleById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetVehicleById.Response>> GetById([FromRoute]GetVehicleById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Vehicle == null)
            {
                return new NotFoundObjectResult(request.VehicleId);
            }

            return response;
        }

        [HttpGet(Name = "GetVehiclesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVehicles.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVehicles.Response>> Get()
            => await _mediator.Send(new GetVehicles.Request());           
    }
}
