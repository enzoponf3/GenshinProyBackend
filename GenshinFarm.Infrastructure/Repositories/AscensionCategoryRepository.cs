using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Infrastructure.Repositories
{
    public class AscensionCategoryRepository : BaseRepository<AscensionCategory>, IAscensionRespository
    {
        public AscensionCategoryRepository(GenshinDbContext context) : base(context)
        {

        }
        public async Task<AscensionCategory> GetByLvl(int lvl)
        {
            return await _entities.FirstOrDefaultAsync(c => c.Category == lvl);
        }
    }
}
