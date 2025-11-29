using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response.User;
using Generic.Comunication.Resources.User;
using Generic.Domain.Entities;
using Generic.Domain.Repositories.User;
using Generic.Domain.Security.Authentication;
using Generic.Domain.Security.Criptography;
using Generic.Exception.BaseExceptions;

namespace Generic.Application.UseCases.User.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IReadOnlyUserRepository _readOnlyUserRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICriptographyService _criptographyService;

        public LoginUseCase(
            IReadOnlyUserRepository readOnlyUserRepository,
            ITokenGenerator tokenGenerator,
            ICriptographyService criptographyService    
        ){
            _criptographyService = criptographyService;
            _tokenGenerator = tokenGenerator;
            _readOnlyUserRepository = readOnlyUserRepository;
        }
        public async Task<ResponseLoggedUserJson> Execute(RequestLoginJson request)
        {
            var user = await HandleLogin(request);

            var token = _tokenGenerator.GenerateToken(user);

            return new ResponseLoggedUserJson
            {
                Id = user.Id,
                Token = token
            };
        }
        
        private async Task<UserEntity> HandleLogin(RequestLoginJson request)
        {
            var validator = new LoginValidator();
            var validationResult = await validator.ValidateAsync(request);

            // Check for validation errors count, cause password validator do not return isValid
            if (validationResult.Errors.Count > 0)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnRequestValidation(errors);
            }

            var user = await _readOnlyUserRepository.GetUserByEmail(request.Email);

            if (user != null && user.IsActive)
            {
                var result = _criptographyService.VerifyPassword(request.Password, user.PasswordHash);
                if (result) return user;
            }
            throw new UnauthorizedError(UserResource.UNAUTHORIZED);
        }
    }
}
