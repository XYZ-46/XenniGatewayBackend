using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class XenniDB(DbContextOptions<XenniDB> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<TenantModel> TenantModel { get; set; }
    }
}
