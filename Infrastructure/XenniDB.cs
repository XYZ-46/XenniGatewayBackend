using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class XenniDB(DbContextOptions<XenniDB> options) : DbContext(options)
    {

        public DbSet<TokenModel> TokenModel { get; set; }
        public DbSet<UserProfileModel> UserProfileModel { get; set; }
        public DbSet<UserLoginModel> UserLoginModel { get; set; }
        public DbSet<TenantModel> TenantModel { get; set; }
    }
}
