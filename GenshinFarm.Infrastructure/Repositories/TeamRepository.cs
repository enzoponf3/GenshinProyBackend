using GenshinFarm.Core.Entities;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Infrastructure.Repositories
{
    public class TeamRepository : BaseRepository<Team> 
    {
        public TeamRepository(GenshinDbContext context) : base(context)
        {

        }

        public override async Task<Team> GetById(string id)
        {
            return await _entities
                            .Include(t => t.CharacterWeapons)
                                .ThenInclude(cw => cw.Character)
                            .Include(t => t.CharacterWeapons)
                                .ThenInclude(cw => cw.Weapon)
                            .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
