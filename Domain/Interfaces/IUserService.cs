using Domain.Entities;
using Infrastructure.Models;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsRegisteredAsync(string email, CancellationToken cancellationToken = default);
        Task<UserCreatedDto> RegisterAsync(UserProfileModel userProfile, string passwordHash, CancellationToken cancellationToken = default);
        Task<string?> GetPasswordActiveAsync(string email, CancellationToken cancellationToken = default);
        Task GenerateToken(CancellationToken cancellationToken = default);
    }
}
