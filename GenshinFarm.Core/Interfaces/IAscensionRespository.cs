using GenshinFarm.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Interfaces
{
    public interface IAscensionRespository : IRepository<AscensionCategory>
    {
        Task<AscensionCategory> GetByLvl(int lvl); 
    }
}
