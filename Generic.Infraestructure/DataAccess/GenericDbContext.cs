using Generic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Generic.Infraestructure.DataAccess
{
    public class GenericDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public GenericDbContext(DbContextOptions<GenericDbContext> options) : base(options){}

    }
}
