using GenshinFarm.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class Material : BaseEntity
    {
        public Material()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Rarity Rarity { get; set; }
        public virtual ICollection<FarmLocation> FarmLocation { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<DaysOfWeek> DaysAvailable { get; set; }
        public virtual ICollection<AscensionCategory> AscensionCategories { get; set; }
    }
    public class AscMaterial : Material
    {

    }
}
