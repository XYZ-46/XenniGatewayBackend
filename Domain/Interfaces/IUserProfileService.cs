using Infrastructure.Models;

namespace Domain.Interfaces
{
    public interface IUserProfileService : IServiceBase<UserProfileModel>
    {
        Task<UserProfileModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
