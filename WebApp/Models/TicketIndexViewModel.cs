using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TicketIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateAtPersian { get; set; }
        public IList<Data.Entities.TicketMessage> TicketMessages { get; set; }
    }
}
