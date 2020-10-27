using SocialMediaCore.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaCore.DTOs
{
    public class SecurityDTO
    {
        public string User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType? Role { get; set; }
    }
}
