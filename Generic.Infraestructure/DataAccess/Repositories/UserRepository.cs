using Generic.Domain.Entities;
using Generic.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Generic.Infraestructure.DataAccess.Repositories
{
    public class UserRepository : IReadOnlyUserRepository, IWriteOnlyUserRepository
    {
        private readonly GenericDbContext _context;
        public UserRepository(GenericDbContext context)
        {
            _context = context;
        }
        public async Task CreateUser(UserEntity user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> emailAlreadyUsed(string email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }

        public Task<UserEntity?> GetUserByEmail(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
