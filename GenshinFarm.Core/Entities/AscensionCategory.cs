using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Core.Entities
{
    public class AscensionCategory : BaseEntity
    {
        public AscensionCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int Category { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
