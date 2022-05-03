using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class TicketIndexViewModel
    {
        public int Id { get; set; }
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name ="دسته بندی")]
        public string CategoryTitle { get; set; }
        [Display(Name ="تاریخ درج")]
        public DateTime CreateAt { get; set; }
        [Display(Name ="وضعیت انجام")]
        public bool IsDone { get; set; }
        public int UserId { get; set; }

    }
}
