using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class TicketMessageIndexViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]

        public string Title { get; set; }
        [Display(Name = "تاریخ درج")]

        public DateTime CreateAt { get; set; }
        [Display(Name = "تاریخ بروزرسانی")]

        public DateTime UpdateAt { get; set; }
    }
}
