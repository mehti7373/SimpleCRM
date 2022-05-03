using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class TicketMessage
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Message { get; set; }
        public string File { get; set; }
        public DateTime CreateAt { get; set; }
        public int UserId { get; set; }
        public TicketSide TicketSide { get; set; }
    }
}
