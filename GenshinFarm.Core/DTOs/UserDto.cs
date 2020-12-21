using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Core.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
