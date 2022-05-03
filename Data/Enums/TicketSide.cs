
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    public enum TicketSide
    {
        [Display(Name ="پشتیبان")]
        AnswerAdmin = 1,
        [Display(Name ="کاربر")]
        RequestUser = 2
    }
}
