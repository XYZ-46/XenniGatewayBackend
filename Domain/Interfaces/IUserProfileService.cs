using Infrastructure.Models;

namespace Domain.Interfaces
{
    public interface IUserProfileService : IServiceDomainBase<UserProfileModel>
    {
        Task<UserProfileModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
