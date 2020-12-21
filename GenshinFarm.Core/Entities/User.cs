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
            this.Characters = new HashSet<Character>();
            this.Weapons = new HashSet<Weapon>();
            this.UserElement = new HashSet<UserElement>();
        }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public RoleType Role { get; set; }
        public DateTime LastTimeLoged { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Weapon> Weapons { get; set; }
        public virtual ICollection<UserElement> UserElement { get; set; }
    }
}
