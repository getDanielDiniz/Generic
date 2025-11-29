using Generic.Application.UseCases.User.Login;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response.User;
using Microsoft.AspNetCore.Mvc;

namespace Generic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseLoggedUserJson))]
        public async Task<IActionResult> Login(
            [FromBody] RequestLoginJson request,
            [FromServices] ILoginUseCase useCase
        )
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
