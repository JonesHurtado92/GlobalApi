using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTickets.Dtos
{
    public interface ITicketPersistence
    {
       IEnumerable<Ticket> GetAllTickets();
       IEnumerable<Ticket> GetTicketByGuid(Guid userId);
       Ticket InsertTicket(Ticket dataToInsert);
       void DeleteTicketByTicketId(Guid ticketGuid);
       Ticket UpdateTicket(Guid ticketGuid, Ticket dataToUpdate);
    }
}
