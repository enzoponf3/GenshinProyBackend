using GenshinFarm.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Character> CharacterRepository { get;}
        IRepository<Weapon> WeaponRepository { get; }
        IRepository<Material> MaterialRepository { get; }
        IRepository<Talent> TalentRepository { get; }
        IRepository<FarmLocation> FarmLocationRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}
