using GenshinFarm.Core.Entities;

namespace GenshinFarm.Infrastructure.Interfaces
{
    public interface IPasswordService
    {
        bool Check(string key, string salt, string password);
        void Hash(User user, string password);
    }
}
