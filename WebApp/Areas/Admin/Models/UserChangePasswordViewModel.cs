using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class UserChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور وارد شده با هم مطابقت ندارد")]
        public string ConfirmPassword { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [StringLength(100, ErrorMessage = "{0} باید بین {2} و {1} کاراکتر باشد", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
