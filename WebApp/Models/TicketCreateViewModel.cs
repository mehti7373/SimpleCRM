using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TicketCreateViewModel
    {

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} را انتخاب نمایید")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "تکمیل فیلد {0} الزامی است")]
        public string Title { get; set; }

        [Display(Name = "پیام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "تکمیل فیلد {0} الزامی است")]
        public string Message { get; set; }
        [Display(Name = "فایل")]
        public IFormFile FormFile { get; set; }
    }
}
