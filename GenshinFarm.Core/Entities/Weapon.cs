using GenshinFarm.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class Weapon : BaseEntity
    {
        public Weapon()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Rarity Rarity { get; set; }
        public WeaponType Type { get; set; }
        public int Attack { get; set; }
        public int PowerLvl { get; set; }
        public string Desciption { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
