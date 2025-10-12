using Domain.Interfaces;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Domain.Services
{
    public class UserLoginService(IUserLoginRepo userLoginRepo) : ServiceDomainBase<UserLoginModel>(userLoginRepo), IUserLoginService
    {
    }
}
