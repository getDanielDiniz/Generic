using Generic.Application.UseCases.User.Register;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response;
using Generic.Comunication.DTO_s.Response.User;
using Microsoft.AspNetCore.Mvc;

namespace Generic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // POST api/user
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(ResponseLoggedUserJson))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseErrorJson))]
        public async Task<IActionResult> Register(
            [FromBody] RequestRegisterUserJson user,
            [FromServices] IRegisterUserUseCase registerUserUseCase
        )
        {
            var result = await registerUserUseCase.Execute(user);
            return Created(string.Empty, result);
        }
    }
}
