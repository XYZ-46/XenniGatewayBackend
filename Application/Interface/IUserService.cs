using Application.ResponseDTO;
using Infrastructure.Entities;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<PagedResult<UserPageDto>> PaginationDefaultAsync(CancellationToken cancellationToken = default);
    }
}
