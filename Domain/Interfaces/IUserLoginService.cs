using Infrastructure.Models;

namespace Domain.Interfaces
{
    public interface IUserLoginService : IServiceBase<UserLoginModel>
    {

        Task<UserLoginModel?> GetByProfileIdAsync(long userProfileId, CancellationToken cancellationToken = default);
        Task<UserLoginModel?> GetByProfileIdActiveAsync(long userProfileId, CancellationToken cancellationToken = default);
    }
}
