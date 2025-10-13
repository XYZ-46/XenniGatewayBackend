using Domain.Interfaces;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Domain.Services
{
    public class UserLoginService(IUserLoginRepo userLoginRepo) : ServiceBase<UserLoginModel>(userLoginRepo), IUserLoginService
    {
        private readonly IUserLoginRepo _userLoginRepo = userLoginRepo;

        public async Task<UserLoginModel?> GetByProfileIdActiveAsync(long userProfileId, CancellationToken cancellationToken = default)
            => await _userLoginRepo.GetByProfileIdActiveAsync(userProfileId, cancellationToken);

        public async Task<UserLoginModel?> GetByProfileIdAsync(long userProfileId, CancellationToken cancellationToken = default)
            => await _userLoginRepo.GetByProfileIdAsync(userProfileId, cancellationToken);

    }
}
