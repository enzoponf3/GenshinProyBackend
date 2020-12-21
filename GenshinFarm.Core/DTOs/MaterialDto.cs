using GenshinFarm.Core.Enumerations;

namespace GenshinFarm.Core.DTOs
{
    public class MaterialDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Rarity Rarity { get; set; }
    }
}
