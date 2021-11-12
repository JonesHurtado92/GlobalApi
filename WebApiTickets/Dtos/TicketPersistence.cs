using MongoDB.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiTickets.Services;

namespace WebApiTickets.Dtos
{
    public class TicketPersistence : ITicketPersistence
    {
        private ITicketService _mongoTicket;
        public TicketPersistence(ITicketService ticketService)
        {
            _mongoTicket = ticketService;
        }

        public void DeleteTicketByTicketId(Guid ticketGuid) => _mongoTicket.DeleteTicketById(ticketGuid);

        public IEnumerable<Ticket> GetAllTickets()
        {
            var result = new List<Ticket>();
            var tickets = _mongoTicket.GetAllTicketsMongo();
            foreach (var t in tickets)
            {
                result.Add(new Ticket
                {
                    TicketId = t.TicketId.ToString(),
                    Username = t.Username,
                    UserId = t.UserId.ToString(),
                    Created = t.Created,
                    LastUpdate = t.LastUpdate,
                    Open = t.Open
                }); ; ;
            }
            return result;
        }

        public IEnumerable<Ticket> GetTicketByGuid(Guid ticketGuid)
        {
            var result = new List<Ticket>();
            var ticket = _mongoTicket.GetTicketById(ticketGuid);
            foreach (var t in ticket)
            {
                result.Add(new Ticket
                {
                    TicketId = t.TicketId.ToString(),
                    Username = t.Username,
                    UserId = t.UserId.ToString(),
                    Created = t.Created,
                    LastUpdate = t.LastUpdate,
                    Open = t.Open
                });
            }
            return result;
        }

        public Ticket InsertTicket(Ticket dataToInsert)
        {
            var userGuid = _mongoTicket.GetGuidByUsername(dataToInsert.Username);
            var insertData = new TicketMongo { TicketId = Guid.NewGuid(), Open = true, Created = DateTime.Now, LastUpdate = null};
            if (userGuid.Any())
            {
                insertData.UserId = userGuid.First().UserId;
                insertData.Username = userGuid.First().Username;
            }
            else
            {
                insertData.UserId = Guid.NewGuid();
                insertData.Username = dataToInsert.Username;
            }

            _mongoTicket.InsertTicket(insertData);
            return new Ticket { 
                Username = insertData.Username, 
                TicketId = insertData.TicketId.ToString("D"),
                UserId = insertData.UserId.ToString("D"),
                Created = insertData.Created,
                LastUpdate = insertData.LastUpdate,
                Open = insertData.Open
            };
        }

        public Ticket UpdateTicket(Guid ticketGuid, Ticket dataToUpdate)
        {
            var ticketData = _mongoTicket.GetTicketById(ticketGuid);           
            if (!ticketData.Any()) return null;
            var updateTicket = new TicketMongo { };
            updateTicket.Username = dataToUpdate.Username;
            updateTicket.UserId = ticketData.First().UserId;
            updateTicket.TicketId = ticketData.First().TicketId;
            updateTicket.Open = dataToUpdate.Open;
            updateTicket.Created = ticketData.First().Created;
            updateTicket.LastUpdate = DateTime.Now;
            updateTicket.Id = ticketData.First().Id;
            _mongoTicket.UpdateTicket(ticketGuid, updateTicket);
            return new Ticket {                
                Username = updateTicket.Username, 
                UserId = updateTicket.UserId.ToString("D"), 
                TicketId = updateTicket.TicketId.ToString("D"),
                Created = updateTicket.Created,
                LastUpdate = updateTicket.LastUpdate,
                Open = updateTicket.Open
            };
        }
    }
}
