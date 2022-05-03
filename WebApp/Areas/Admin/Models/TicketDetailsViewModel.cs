using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class TicketDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreateAt { get; set; }
        public IList<Data.Entities.TicketMessage> TicketMessages { get; set; }
    }
}
