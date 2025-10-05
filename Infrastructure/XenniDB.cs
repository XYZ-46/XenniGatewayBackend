using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class XenniDB(DbContextOptions<XenniDB> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
