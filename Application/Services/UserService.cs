using Application.Interface;
using Application.ResponseDTO;
using Infrastructure.Entities;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService(IUserPageRepo userPageRepo) : IUserService
    {
        private readonly IUserPageRepo _userPageRepo = userPageRepo;

        public async Task<PagedResult<UserPageDto>> PaginationDefaultAsync(CancellationToken cancellationToken = default)
        {
            PagedResult<UserPageDto> userPage = new();
            var userData = await _userPageRepo.GetQueryDefault().ToListAsync(cancellationToken);

            userPage.Items = userData;

            return userPage;
        }

        public async Task<PagedResult<UserPageDto>> PaginationAsync(CancellationToken cancellationToken = default)
        {
            PagedResult<UserPageDto> userPage = new();
            var userData = await _userPageRepo.GetQueryDefault().ToListAsync(cancellationToken);

            userPage.Items = userData;



            return userPage;
        }

    }
}
