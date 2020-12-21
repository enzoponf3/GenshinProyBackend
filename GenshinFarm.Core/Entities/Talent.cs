using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class Talent : BaseEntity
    {
        public Talent()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Level { get; set; }
        public int PowerLvl { get; set; }
        public Character Character { get; set; }
    }
}
