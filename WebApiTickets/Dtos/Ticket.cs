using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTickets.Dtos
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool Open { get; set; }
        public DateTime Created {get; set;}
        public DateTime? LastUpdate {get; set;}

    }
}
