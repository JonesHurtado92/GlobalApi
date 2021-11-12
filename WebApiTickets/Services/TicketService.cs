using MongoDB.DataContracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTickets.DataContracts;
using WebApiTickets.Settings;

namespace WebApiTickets.Services
{
    public class TicketService : ITicketService
    {
        private IMongoCollection<TicketMongo> _ticket;
        private const string _ticketCollection = "Tickets";

        public TicketService(IMongoSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _ticket = database.GetCollection<TicketMongo>(_ticketCollection);
        }

        public IEnumerable<TicketMongo> GetAllTicketsMongo() => _ticket.Find(d => true).ToEnumerable();

        public IEnumerable<TicketMongo> GetTicketById(Guid ticketGuid) => _ticket.Find(t => t.TicketId.Equals(ticketGuid)).ToEnumerable();

        public IEnumerable<TicketMongo> GetGuidByUsername(string username) => _ticket.Find(t => t.Username.Equals(username)).ToEnumerable();

        public void InsertTicket(TicketMongo insertData) => _ticket.InsertOne(insertData);

        public void DeleteTicketById(Guid ticketGuid) => _ticket.DeleteOne(d => d.TicketId.Equals(ticketGuid));

        public void UpdateTicket(Guid ticketGuid, TicketMongo updateTicket) => _ticket.ReplaceOne(u => u.TicketId.Equals(ticketGuid), updateTicket);
    }
}
