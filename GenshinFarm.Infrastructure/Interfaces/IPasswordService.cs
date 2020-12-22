using System.Collections.Generic;

namespace GenshinFarm.Infrastructure.Interfaces
{
    public interface IPasswordService
    {
        bool Check(string key, string salt, string password);
        List<string> Hash(string password);
    }
}
