using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public bool IsOpen { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? AdminRepliedUserId { get; set; }
        public User AdminRepliedUser { get; set; }
        public DateTime? ReplyAt { get; set; }
        public IList<TicketMessage> TicketMessages { get; set; }

    }
}
