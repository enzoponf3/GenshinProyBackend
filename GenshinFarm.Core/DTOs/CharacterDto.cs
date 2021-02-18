using GenshinFarm.Core.Enumerations;

namespace GenshinFarm.Core.DTOs
{
    public class CharacterDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Rarity Rarity { get; set; }
        public string Type { get; set; }
        public string WeaponType { get; set; }
        public string Description { get; set; }
    }
}
