using AutoMapper;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response.User;
using Generic.Comunication.Resources.User;
using Generic.Domain.Repositories;
using Generic.Domain.Repositories.User;
using Generic.Domain.Security.Authentication;
using Generic.Domain.Security.Criptography;
using Generic.Exception.BaseExceptions;
using FluentValidation.Results;

namespace Generic.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IWriteOnlyUserRepository _writeOnlyUserRepository;
        private readonly IReadOnlyUserRepository _readOnlyUserRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ICriptographyService _criptographyService;
        private readonly ITokenGenerator _tokenGenerator;

        public RegisterUserUseCase(
            IReadOnlyUserRepository readOnlyUserRepository,
            IWriteOnlyUserRepository writeOnlyUserRepository,
            IUnityOfWork unityOfWork,
            IMapper mapper,
            ICriptographyService criptographyService,
            ITokenGenerator tokenGenerator
            )
        {
            _writeOnlyUserRepository = writeOnlyUserRepository;
            _readOnlyUserRepository = readOnlyUserRepository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _criptographyService = criptographyService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseLoggedUserJson> Execute(RequestRegisterUserJson request)
        {
            await ValidateRequest(request);

            var user = _mapper.Map<Domain.Entities.UserEntity>(request);
            user.IsActive = true;
            user.SecurityId = Guid.NewGuid();
            user.PasswordHash = _criptographyService.HashPassword(request.Password);

            await _writeOnlyUserRepository.CreateUser(user);
            var wasSuccessful = await _unityOfWork.Commit();

            var response = new ResponseLoggedUserJson()
            {
                Id = user.Id,
                Token = _tokenGenerator.GenerateToken(user)
            };

            return response;
        }
    

        private async Task ValidateRequest(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var validationResult = validator.Validate(request);
            
            // Check don't use .isValid to ensure errors from custom password validator are included
            if (validationResult.Errors.Count > 0)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnRequestValidation(errors);
            }

            var emailAlreadyUsed = await _readOnlyUserRepository.EmailAlreadyUsed(request.Email);
            if (emailAlreadyUsed) throw new ErrorOnInformationConflict(UserResource.EMAIL_ALREADY_USED);            
        }
    }
}
