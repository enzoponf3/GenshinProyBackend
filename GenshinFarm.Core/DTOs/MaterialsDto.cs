using GenshinFarm.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Core.DTOs
{
    public class MaterialsDto
    {
        public IEnumerable<Material> CharacterMaterials { get; set; }
        public IEnumerable<Material> WeaponMaterials { get; set; }
    }
}
