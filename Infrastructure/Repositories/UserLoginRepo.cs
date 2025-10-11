using AbstractionBase;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public class UserLoginRepo(XenniDB _xenniDB) : RepositoryBase<UserLoginModel>(_xenniDB), IUserLoginRepo
    {
    }
}
