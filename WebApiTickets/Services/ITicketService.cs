using MongoDB.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTickets.Services
{
    public interface ITicketService
    {
        IEnumerable<TicketMongo> GetAllTicketsMongo();
        IEnumerable<TicketMongo> GetTicketById(Guid ticketGuid);
        IEnumerable<TicketMongo> GetGuidByUsername(string username);
        void InsertTicket(TicketMongo insertData);
        void DeleteTicketById(Guid ticketGuid);
        void UpdateTicket(Guid ticketGuid, TicketMongo updateTicket);
    }
}
