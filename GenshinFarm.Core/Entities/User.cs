using GenshinFarm.Core.Enumerations;
using System;
using System.Collections.Generic;

namespace GenshinFarm.Core.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();    
            this.Teams = new HashSet<Team>();
        }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public RoleType Role { get; set; }
        public DateTime LastTimeLoged { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<CharacterWeapon> CharacterWeapons { get; set; }
    }
}
