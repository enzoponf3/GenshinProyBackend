using GenshinFarm.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class Character : BaseEntity
    {
        public Character()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Talents = new HashSet<Talent>();
            this.TalentMaterials = new HashSet<Material>();
            this.AscensionMaterials = new HashSet<AscMaterial>();
        }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Rarity Rarity { get; set; }
        public ElementalType Type { get; set; }
        public WeaponType WeaponType { get; set; }
        public virtual ICollection<Material> TalentMaterials { get; set; }
        public virtual ICollection<Talent> Talents { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<AscMaterial> AscensionMaterials { get; set; }
        public virtual ICollection<CharacterWeapon> CharacterWeapon { get; set; }
    }
}
