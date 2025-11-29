using Generic.Comunication.DTO_s.Request.User;

namespace Generic.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
