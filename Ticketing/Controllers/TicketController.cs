using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Core.DTOs;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Entities.Ticket;

namespace Ticketing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketController> _logger;
        private readonly IUserServices _userServices;

        public TicketController(ITicketService ticketService, IMapper mapper, ILogger<TicketController> logger, IUserServices userServices)
        {
            _ticketService = ticketService;
            _mapper = mapper;
            _logger = logger;
            _userServices = userServices;
        }


        #region Get

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var result = await _ticketService.GetTicketsAsync();
            return Ok(result);
        }
        
        [HttpGet("{id}",Name = "GetTicketById")]
        public async Task<IActionResult> GetTicketById(int id,bool includeAnswerAndQuestions = false)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id,includeAnswerAndQuestions);

            if (ticket == null)
                return NotFound(ticket);

            return Ok(ticket);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult> AddTicket(TicketCreationDto inputDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //Checking is inputUserId exist in database or not !!
            if (!await _userServices.IsExistUser(inputDto.userId))
                return BadRequest();
            
            var ticket = _mapper.Map<Ticket>(inputDto);

            var ticketId = await _ticketService.AddTicketAsync(ticket);

            return CreatedAtRoute("GetTicketById",new
            {
                id = ticketId
            }, inputDto);
        }

        #endregion

        #region Put

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicket(int id ,TicketCreationDto inputDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            ticket.Title = inputDto.Title;
            ticket.IsFinished = inputDto.IsFinished;
            ticket.userId = inputDto.userId;
            
            _ticketService.UpdateTicket(ticket);
            
            return NoContent();
        }

        #endregion

        #region Patch

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateTicket(int id, JsonPatchDocument<TicketCreationDto> patchDocument)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();
            
            //need this variable for "ApplyTo" function
            //store old values in it 
            var ticketToPatch = new TicketCreationDto()
            {
                Title = ticket.Title,
                userId = ticket.userId,
                IsFinished = ticket.IsFinished
            };
            
            patchDocument.ApplyTo(ticketToPatch,ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
            
            //Validate New Values by modelState
            if (!TryValidateModel(ticketToPatch))
                return BadRequest(modelState: ModelState);

            ticket.Title = ticketToPatch.Title;
            ticket.userId = ticketToPatch.userId;
            ticket.IsFinished = ticketToPatch.IsFinished;
            
            _ticketService.UpdateTicket(ticket);
            
            return NoContent();
        }
        

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            
            _ticketService.DeleteTicket(ticket);
            
            //Log a warning to know
            _logger.LogWarning($"Ticket with id {id} has been deleted");
            
            return NoContent();
        }

        #endregion
    }
}
