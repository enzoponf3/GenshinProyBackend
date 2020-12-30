using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenshinFarm.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GenshinDbContext context) : base (context)
        {

        }
        public async Task<User> LoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Username == login.Username) ?? await _entities.FirstOrDefaultAsync(x => x.Email == login.Username);
        }
    }
}
