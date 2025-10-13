using Domain.Interfaces;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Domain.Services
{
    public class UserProfileService(IUserProfileRepo userProfileRepo) : ServiceBase<UserProfileModel>(userProfileRepo), IUserProfileService
    {
        private readonly IUserProfileRepo _userProfileRepo = userProfileRepo;

        public async Task<UserProfileModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) => await _userProfileRepo.GetByEmailAsync(email, cancellationToken);
    }
}
