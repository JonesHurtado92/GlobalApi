using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiTickets.Dtos;
using WebApiTickets.Services;
using WebApiTickets.Wrappers;

namespace WebApiTickets.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketPersistence _ticketPersistence;

        public TicketController(ITicketPersistence ticketPersistence)
        {
            _ticketPersistence = ticketPersistence;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Ticket>> GetAllTickets([FromQuery] PageFilter filter)
        {
            var filerPagination = new PageFilter(filter.PageNumber, filter.PageSize); 
            var result = _ticketPersistence.GetAllTickets().Skip((filerPagination.PageNumber - 1) * filerPagination.PageSize).Take(filerPagination.PageSize);            
            return Ok(new PagedConfig<IEnumerable<Ticket>>(result, filerPagination.PageNumber, filerPagination.PageSize));
        }

        [HttpGet("GetTicket/{ticketId}")]
        public ActionResult<IEnumerable<Ticket>> GetTicketsByUsername([FromRoute, MinLength(36), MaxLength(36)] string ticketId)
        {
            if (string.IsNullOrEmpty(ticketId) || !Guid.TryParse(ticketId, out var guidTicket)) return BadRequest();
            var result = _ticketPersistence.GetTicketByGuid(guidTicket);
            return Ok(new Response<IEnumerable<Ticket>>(result));
        }

        [HttpPost("InsertRegister")]
        public ActionResult<Ticket> InsertTicket([FromBody] Ticket dataToInsert)
        {
            if (dataToInsert is null) BadRequest();

            if(string.IsNullOrEmpty(dataToInsert.Username))
            {
                ModelState.AddModelError("Username", "Must Insert an Username");
                return BadRequest(ModelState);
            }            

            _ticketPersistence.InsertTicket(dataToInsert);
            return NoContent();
        }

        [HttpDelete("Delete/{ticketId}")]
        public ActionResult<IEnumerable<Ticket>> DeleteTicketByTicketId([FromRoute, MinLength(36), MaxLength(36)] string ticketId)
        {
            if (string.IsNullOrEmpty(ticketId) || !Guid.TryParse(ticketId, out var ticketGuid)) return BadRequest();
            _ticketPersistence.DeleteTicketByTicketId(ticketGuid);
            return NoContent();
        }

        [HttpPut("UpdateRegister")]
        public ActionResult<Ticket> UpdateRegister([FromBody] Ticket dataToUpdate)
        {
            if (dataToUpdate is null) BadRequest();

            if (string.IsNullOrEmpty(dataToUpdate.TicketId) || !Guid.TryParse(dataToUpdate.TicketId, out var ticketGuid))
            {
                ModelState.AddModelError("TicketId", "Must Insert a TicketId Valid and not empty");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(dataToUpdate.Username))
            {
                ModelState.AddModelError("Username", "Must insert an username");
                return BadRequest(ModelState);
            }

            var result = _ticketPersistence.UpdateTicket(ticketGuid, dataToUpdate);
            return Ok(new Response<Ticket>(result));
        }
    }
}
