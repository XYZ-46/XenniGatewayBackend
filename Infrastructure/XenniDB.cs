using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class XenniDB(DbContextOptions<XenniDB> options) : DbContext(options)
    {

        public DbSet<TenantModel> TenantModel { get; set; }
    }
}
