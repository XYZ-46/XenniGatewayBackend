using ApiService.DTO.Request;
using ApiService.DTO.Response;
using Application.Interface;
using Application.ResponseDTO;
using Domain.Exception;
using Infrastructure.Database;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    public class UserController(IUserService userService, XenniDB context) : BaseApiController
    {
        private readonly IUserService _userService = userService;
        protected readonly XenniDB _context = context;

        [HttpGet("page")]
        public async Task<IActionResult> Page(PageReq PageRequest, CancellationToken cancellationToken)
        {
            PagedResult<UserPageDto> userPage;
            if (PageRequest.IsDefault )
            {
                userPage = await _userService.PaginationDefaultAsync(cancellationToken) ?? throw new XenniNotFoundException();
                userPage.PageNumber = 1;
                userPage.PageSize = 5;
            }
            else
            {
                throw new XenniException("Not implement");
            }

            if (!userPage.Items.Any()) throw new XenniNotFoundException();

            return new JsonResult(ApiResponseDefault<object>.Success(userPage, message: null));

        }
    }
}