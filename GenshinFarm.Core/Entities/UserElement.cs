using System;

namespace GenshinFarm.Core.Entities
{
    public class UserElement : BaseEntity
    {
        public UserElement(int lvl)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Lvl = lvl;
            setPowerLvl();
        }

        public void setPowerLvl()
        {
            if(Lvl > 90 || Lvl < 0) { throw new ArgumentOutOfRangeException("Lvl must be between 0 and 90"); }
            int result = Lvl > 20 ? 1 : 0;
            result = Lvl > 40 ? 2 : result;
            result = Lvl > 50 ? 3 : result;
            result = Lvl > 60 ? 4 : result;
            result = Lvl > 70 ? 5 : result;
            result = Lvl > 80 ? 6 : result;
            this.PowerLvl = result;
        }

        public int Lvl { get; private set; }
        public int PowerLvl { get; private set; }
        public string ElementId { get; set; }
        public virtual User User { get; set; }
    }
}
