using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class FarmLocation : BaseEntity
    {
        public FarmLocation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Materials = new HashSet<Material>();
        }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Weekly { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
