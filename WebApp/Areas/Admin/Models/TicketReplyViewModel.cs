using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class TicketReplyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "پیام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "تکمیل فیلد {0} الزامی است")]
        public string Message { get; set; }
        [Display(Name = "فایل")]
        public IFormFile FormFile { get; set; }
    }
}
