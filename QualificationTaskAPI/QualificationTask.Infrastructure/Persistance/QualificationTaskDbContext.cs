using Microsoft.EntityFrameworkCore;
using QualificationTask.Domain.Entities;

namespace QualificationTask.Infrastructure.Persistance
{
    public class QualificationTaskDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public QualificationTaskDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
