using Generic.Domain.Repositories;
using Generic.Domain.Repositories.User;
using Generic.Domain.Security.Authentication;
using Generic.Domain.Security.Criptography;
using Generic.Infraestructure.DataAccess;
using Generic.Infraestructure.DataAccess.Repositories;
using Generic.Infraestructure.Security.Authentication;
using Generic.Infraestructure.Security.Criptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Generic.Infraestructure
{
    public static class AddInfraestructureDependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            AddRepositories(services);
            AddServices(services,configuration);

            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {            
            //User
            services.AddScoped<IWriteOnlyUserRepository, UserRepository>();
            services.AddScoped<IReadOnlyUserRepository, UserRepository>();

            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<GenericDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICriptographyService, CriptographyService>();

            services.AddScoped<ITokenGenerator>(DI => new TokenGenerator(
                minutesToExpire: int.Parse(configuration["Auth:Jwt:ExpireInMinutes"]!),
                secretKey: configuration["Auth:Jwt:SigningKey"]!
            ));

            services.AddScoped<ILoggedUserProvider, LoggedUserProvider>();
        }
    }

    
}
