using Infrastructure.Models;

namespace Infrastructure.IRepositories
{
    public interface IUserLoginRepo : IRepositoryBase<UserLoginModel>
    {
        Task<UserLoginModel?> GetByProfileIdAsync(long userProfileId, CancellationToken cancellationToken = default);
        Task<UserLoginModel?> GetByProfileIdActiveAsync(long userProfileId, CancellationToken cancellationToken = default);
    }
}
