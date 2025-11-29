using Generic.Application.UseCases.User.ChangePassword;
using Generic.Application.UseCases.User.Register;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response;
using Generic.Comunication.DTO_s.Response.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Generic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region SwaggerDocumentation
        /// <summary>
        /// Endpoint para registrar um novo usuário.
        /// </summary>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos enviados para a requisição.</response>
        #endregion
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

        #region SwaggerDocumentation
        /// <summary>
        /// Endpoint para alterar a senha de um usuário.
        /// </summary>
        /// <response code="204">Senha Alterada.</response>
        /// <response code="400">Erro na validação da senha nova e/ou atual.</response>        
        /// <response code="401">Senha atual incorreta.</response>
        #endregion
        [Authorize]
        [HttpPost("Id")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseErrorJson))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseErrorJson))]
        public async Task<IActionResult> ChangePassword(
            [FromBody] RequestChangePasswordJson request,
            [FromServices] IChangePasswordUseCase useCase
        )
        {
            await useCase.Execute(request);
            return NoContent();
        }
    }
}
