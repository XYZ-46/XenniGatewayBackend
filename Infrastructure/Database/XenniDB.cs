using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class XenniDB(DbContextOptions<XenniDB> options) : DbContext(options)
    {
        /// Entity By Query
        public DbSet<UserPageDto> UserPage { get; set; }

        /// Table Database
        public DbSet<TokenModel> TokenModel { get; set; }
        public DbSet<UserProfileModel> UserProfileModel { get; set; }
        public DbSet<UserLoginModel> UserLoginModel { get; set; }
        public DbSet<TenantModel> TenantModel { get; set; }
    }
}
