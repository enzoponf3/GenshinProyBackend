using GenshinFarm.Core.Entities;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetLoginByCredentials(UserLogin login);
    }
}
