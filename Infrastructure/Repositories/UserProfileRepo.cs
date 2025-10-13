using Infrastructure.IRepositories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserProfileRepo(XenniDB _xenniDB) : RepositoryBase<UserProfileModel>(_xenniDB), IUserProfileRepo
    {
        public async Task<UserProfileModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _set.FirstOrDefaultAsync(z => z.Email == email, cancellationToken: cancellationToken);
    }
}