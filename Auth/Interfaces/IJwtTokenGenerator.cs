using Infrastructure.Models;

namespace Auth.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserProfileModel userProfile);
    }
}
