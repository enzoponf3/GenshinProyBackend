using GenshinFarm.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task AddElement(string userId, UserElement userElementDto);
        Task AddElements(string userId, ICollection<UserElement> userElements);
        Task<User> GetLoginByCredentials(UserLogin login);
    }
}
