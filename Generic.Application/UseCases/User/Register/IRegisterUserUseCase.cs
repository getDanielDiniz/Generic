using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response.User;

namespace Generic.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseLoggedUserJson> Execute(RequestRegisterUserJson request);
    }
}
