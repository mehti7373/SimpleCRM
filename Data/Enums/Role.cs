using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    public enum Role
    {
        [Display(Name = "مدیر")]
        Administrator = 1,

        [Display(Name = "پشتیبان")]
        Supporter = 2,

        [Display(Name = "کاربر")]
        User = 3

    }
}
