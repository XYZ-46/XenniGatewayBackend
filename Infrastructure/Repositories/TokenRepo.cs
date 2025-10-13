using Infrastructure.Database;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public class TokenRepo(XenniDB _xenniDB) : RepositoryBase<TokenModel>(_xenniDB), ITokenRepo
    {
    }
}
