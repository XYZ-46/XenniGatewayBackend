using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class UserDomainService(XenniDB xenniDB, IUserLoginService userLoginService, IUserProfileService userProfileService) : IUserDomainService
    {
        private readonly IUserLoginService _userLoginService = userLoginService;
        private readonly IUserProfileService _userProfileService = userProfileService;

        protected readonly DbContext _xenniDB = xenniDB;

        public async Task<UserCreatedDto> RegisterAsync(UserProfileModel userProfile, string passwordHash, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userProfileService.GetByEmailAsync(userProfile.Email);
            if (existingUser is not null) throw new XenniException("User Email already registered.");

            await using var transaction = await _xenniDB.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _userProfileService.AddAsync(userProfile);
                await _userLoginService.AddAsync(new UserLoginModel() { UserProfileId = userProfile.Id, PasswordHash = passwordHash });

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
    }
}
