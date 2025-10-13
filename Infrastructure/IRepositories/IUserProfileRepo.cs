using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.IRepositories
{
    public interface IUserProfileRepo : IRepositoryBase<UserProfileModel>
    {
        Task<UserProfileModel?> GetByEmailAsync(string email, CancellationToken cancellationToken=default);
        Task<UserSession?> GetUserIdentity(long UserId, CancellationToken cancellationToken = default);
    }
}
