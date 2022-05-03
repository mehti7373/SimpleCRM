using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public class CookieConfiguration
    {
        public string AccessDeniedPath { get; set; }
        public int LoginExpirationDays { get; set; }
        public string LoginPath { get; set; }
        public string LogoutPath { get; set; }
    }
}
