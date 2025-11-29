using Generic.Application.Mapping;
using Generic.Application.UseCases.User.ChangePassword;
using Generic.Application.UseCases.User.Login;
using Generic.Application.UseCases.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Generic.Application
{
    public static class AddApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapperProfiles(services);

            return services;
        }

        private static void AddUseCases(IServiceCollection services)
        {
            // User
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        }

        private static void AddAutoMapperProfiles(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

        }
    }
}
