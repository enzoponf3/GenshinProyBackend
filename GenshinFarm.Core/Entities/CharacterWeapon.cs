using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class CharacterWeapon : BaseEntity
    {
        public CharacterWeapon(int lvl)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Lvl = lvl;
            setPowerLvl(lvl);
        }

        public void setPowerLvl(int lvl)
        {            
            this.PowerLvl = PowerByLvl(lvl);
        }

        public void setWeaponPower(int lvl)
        {
            this.WeaponPowerLvl = PowerByLvl(lvl);
        }

        private int PowerByLvl(int lvl)
        {
            if (lvl > 90 || lvl < 0) { throw new ArgumentOutOfRangeException("Lvl must be between 0 and 90"); }
            int result = lvl > 20 ? 1 : 0;
            result = lvl > 40 ? 2 : result;
            result = lvl > 50 ? 3 : result;
            result = lvl > 60 ? 4 : result;
            result = lvl > 70 ? 5 : result;
            result = lvl > 80 ? 6 : result;
            return result;
        }

        public int Lvl { get; private set; }
        public int PowerLvl { get; private set; }
        public int WeaponPowerLvl { get; private set; }
        public virtual Weapon Weapon { get; set;}
        public virtual Character Character { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual User User { get; set; }
    }
}
