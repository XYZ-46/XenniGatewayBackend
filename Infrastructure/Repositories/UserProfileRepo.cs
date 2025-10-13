using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserProfileRepo(XenniDB _xenniDB, ITenantRepo tenantRepo) : RepositoryBase<UserProfileModel>(_xenniDB), IUserProfileRepo
    {

        private readonly ITenantRepo _tenantRepo = tenantRepo;

        public override async Task<UserProfileModel?> GetByIdActiveAsync(long id, CancellationToken canceltoken = default)
            => await _set.FirstOrDefaultAsync(z => z.Id == id && !z.IsDeleted && z.IsActive, cancellationToken: canceltoken);

        public async Task<UserProfileModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _set.FirstOrDefaultAsync(z => z.Email == email && !z.IsDeleted && z.IsActive, cancellationToken: cancellationToken);

        public async Task<UserSession?> GetUserIdentity(long UserId, CancellationToken cancellationToken = default)
        {
            var userProfile = await GetByIdActiveAsync(UserId, cancellationToken);
            if (userProfile == null) return null;

            var tenant = await _tenantRepo.GetByIdAsync(userProfile.TenantId, cancellationToken);
            if (tenant == null) return null;


            return new UserSession()
            {
                UserId = userProfile.Id,
                Fullname = userProfile.FullName,
                Email = userProfile.Email,
                TenantId = tenant.Id,
                TenantName = tenant.TenantName,
                Role = ""
            };
        }
    }
}