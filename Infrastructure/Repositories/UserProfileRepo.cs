using AbstractionBase;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public class UserProfileRepo(XenniDB _xenniDB) : RepositoryBase<UserProfileModel>(_xenniDB), IUserProfileRepo

    {
    }
}
