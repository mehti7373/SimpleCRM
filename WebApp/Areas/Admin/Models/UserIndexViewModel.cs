using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class UserIndexViewModel
    {
        public int Id { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "تاریخ درج")]
        public DateTime CreateAt { get; set; }
        [Display(Name = "نقش")]
        public Role Role { get; set; }
    }
}
