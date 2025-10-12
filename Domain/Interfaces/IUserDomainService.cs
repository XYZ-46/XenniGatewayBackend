using Domain.Entities;
using Infrastructure.Models;

namespace Domain.Interfaces
{
    public interface IUserDomainService
    {
        Task<UserCreatedDto> RegisterAsync(UserProfileModel userProfile, string passwordHash, CancellationToken cancellationToken = default);
    }
}
