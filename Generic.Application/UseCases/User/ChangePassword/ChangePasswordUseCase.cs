using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.Resources.User;
using Generic.Domain.Entities;
using Generic.Domain.Repositories.User;
using Generic.Domain.Security.Authentication;
using Generic.Domain.Security.Criptography;
using Generic.Exception.BaseExceptions;

namespace Generic.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        #region DI
        private readonly IReadOnlyUserRepository _readOnlyUserRepository;
        private readonly ICriptographyService _criptographyService;
        private readonly IWriteOnlyUserRepository _writeOnlyUserRepository;
        private readonly ILoggedUserProvider _userProvider;
        public ChangePasswordUseCase(
            IReadOnlyUserRepository readOnlyUserRepository,
            ICriptographyService criptographyService,
            IWriteOnlyUserRepository writeOnlyUserRepository,
            ILoggedUserProvider userProvider
        )
        {
            _readOnlyUserRepository = readOnlyUserRepository;
            _criptographyService = criptographyService;
            _writeOnlyUserRepository = writeOnlyUserRepository;
            _userProvider = userProvider;
        }
        #endregion

        public async Task Execute(RequestChangePasswordJson request)
        {
            ChangePasswordValidator validator = new();
            var result = validator.Validate( request );

            if (!result.IsValid)
                throw new ErrorOnRequestValidation(
                    [.. result.Errors.Select(e => e.ErrorMessage)]
                );

            UserEntity userAuthenticated = await _userProvider.Get();

            bool isMatch = _criptographyService
                .VerifyPassword(request.oldPassword, userAuthenticated.PasswordHash);

            if (!isMatch)
                throw new UnauthorizedError(UserResource.UNAUTHORIZED);

            await _writeOnlyUserRepository.SetNewPassword(
                userAuthenticated.Id,
                _criptographyService
                .HashPassword(request.newPassword)    
            );
        }
    }
}
