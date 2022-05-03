using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateAt { get; set; }
        public Role Role { get; set; }
    }
}
