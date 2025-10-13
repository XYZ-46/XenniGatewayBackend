using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class UserService(XenniDB xenniDB, IUserLoginService userLoginService, IUserProfileService userProfileService) : IUserService
    {
        private readonly IUserLoginService _userLoginService = userLoginService;
        private readonly IUserProfileService _userProfileService = userProfileService;

        protected readonly DbContext _xenniDB = xenniDB;

        public async Task<UserCreatedDto> RegisterAsync(UserProfileModel userProfile, string passwordHash, CancellationToken cancellationToken = default)
        {
            if (await IsRegisteredAsync(userProfile.Email, cancellationToken)) throw new XenniException("User Email already registered.");

            await using var transaction = await _xenniDB.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _userProfileService.AddAsync(userProfile, cancellationToken);

                var loginExist = await _userLoginService.GetByIdAsync(userProfile.Id, cancellationToken);
                if (loginExist is null)
                {
                    await _userLoginService.AddAsync(new UserLoginModel() { UserProfileId = userProfile.Id, PasswordHash = passwordHash }, cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }


            UserCreatedDto userCreated = new()
            {
                UserId = userProfile.Id,
                NickName = userProfile.NickName,
                FullName = userProfile.FullName,
                Email = userProfile.Email,
                TenantId = userProfile.TenantId,
                TenantName = "",
                CreatedDate = userProfile.CreatedDate,
                CreatedById = userProfile.CreatedBy,
                CreatedByName = ""
            };

            return userCreated;
        }

        public async Task<bool> IsRegisteredAsync(string email, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userProfileService.GetByEmailAsync(email, cancellationToken);
            return (existingUser is not null);
        }

        public async Task<string?> GetPasswordActiveAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userProfileService.GetByEmailAsync(email, cancellationToken);
            if (user is null) return null;

            var userlogin = await _userLoginService.GetByProfileIdActiveAsync(user.Id, cancellationToken);
            return userlogin?.PasswordHash;
        }

        public Task GenerateToken(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
