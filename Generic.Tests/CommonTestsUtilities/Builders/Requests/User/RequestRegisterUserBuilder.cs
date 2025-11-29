using Bogus;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Domain.Entities;

namespace CommonTestsUtilities.Builders.Requests.User
{
    public class RequestRegisterUserBuilder
    {
        public static RequestRegisterUserJson Build()
        {
            var faker = new Faker<RequestRegisterUserJson>();

            faker
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password(length:10, prefix: "5@"))
                .RuleFor(u => u.Username, f => f.Name.FirstName());

            return faker.Generate();
        }
    }
}
