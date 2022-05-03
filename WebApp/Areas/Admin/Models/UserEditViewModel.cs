using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class UserEditViewModel
    {
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "تکمیل فیلد {0} الزامی است")]
        public string FirstName { get; set; }
        [Display(Name ="نقش")]
        public Role Role { get; set; }
        public int Id { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "ممنوع شده")]
        public bool LockoutEnabled { get; set; }
    }
}
