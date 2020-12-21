using System;

namespace GenshinFarm.Core.Entities
{
    public class UserElement : BaseEntity
    {
        public UserElement(int lvl)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Lvl = lvl;
            setPowerLvl(Lvl);
        }

        public void setPowerLvl(int lvl)
        {
            if(lvl > 90 || lvl < 0) { throw new ArgumentOutOfRangeException("lvl", "Lvl must be between 0 and 90"); }
            int result = lvl > 20 ? 1 : 0;
            result = lvl > 40 ? 2 : result;
            result = lvl > 50 ? 3 : result;
            result = lvl > 60 ? 4 : result;
            result = lvl > 70 ? 5 : result;
            result = lvl > 80 ? 6 : result;
            this.PowerLvl = result;
        }

        public int Lvl { get; private set; }
        public int PowerLvl { get; private set; }
        public string ElementId { get; set; }
        public virtual User User { get; set; }
    }
}
