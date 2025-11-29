using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response.User;

namespace Generic.Application.UseCases.User.Login
{
    public interface ILoginUseCase
    {
        Task<ResponseLoggedUserJson> Execute(RequestLoginJson request);
    }
}
