using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenshinFarm.Core.Entities
{
    public class Team : BaseEntity
    {
        public Team()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CharacterWeapons = new HashSet<CharacterWeapon>();
        }
        public bool AddCharacter(CharacterWeapon characterWeapon)
        {
            var character = CharacterWeapons.FirstOrDefault(c => c.Character == characterWeapon.Character);
            if(CharacterWeapons.Count < 5 && character != null)
            {
                CharacterWeapons.Add(characterWeapon);
                return true;
            }
            return false;
        }
        public bool RemoveCharacter(CharacterWeapon characterWeapon)
        {
            if (CharacterWeapons.Contains(characterWeapon))
            {
                CharacterWeapons.Remove(characterWeapon);
                return true;
            }
            return false;
        }
        public virtual ICollection<CharacterWeapon> CharacterWeapons { get; set; }
        public virtual User User { get; set; }
    }
}
