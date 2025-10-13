using Infrastructure.IRepositories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserLoginRepo(XenniDB _xenniDB) : RepositoryBase<UserLoginModel>(_xenniDB), IUserLoginRepo
    {
        public async Task<UserLoginModel?> GetByProfileIdActiveAsync(long userProfileId, CancellationToken cancellationToken = default)
            => await _set.FirstOrDefaultAsync(z => z.UserProfileId == userProfileId && !z.IsDeleted, cancellationToken: cancellationToken);

        public async Task<UserLoginModel?> GetByProfileIdAsync(long userProfileId, CancellationToken cancellationToken = default)
          => await _set.FirstOrDefaultAsync(z => z.UserProfileId == userProfileId, cancellationToken: cancellationToken);
    }
}
