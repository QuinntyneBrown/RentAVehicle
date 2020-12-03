using Microsoft.AspNetCore.Mvc;

namespace GoalSetter.Api.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorsController: ControllerBase
    {
        public IActionResult Error() { 
            return Problem();
        }
    }
}
