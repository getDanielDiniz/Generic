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

        public async Task<bool> EmailAlreadyUsed(string email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string?> GetUserPwdById(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.PasswordHash)
                .FirstOrDefaultAsync();
        }

        public async Task SetNewPassword(int id, string newPassword)
        {
            await _context.Users.Where(u => u.Id == id)
                .ExecuteUpdateAsync(
                    u => u.SetProperty(u=> u.PasswordHash,newPassword)
                );
        }
    }
}
