using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class DaysOfWeek : BaseEntity
    {
        public DaysOfWeek()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Materials = new HashSet<Material>();
        }
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
