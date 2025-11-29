using Generic.Domain.Repositories;

namespace Generic.Infraestructure.DataAccess
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly GenericDbContext _context;
        public UnityOfWork(GenericDbContext context)
        {
            _context = context;
        }
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
