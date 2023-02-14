using Microsoft.AspNetCore.Mvc;
using Ticketing.Core.DTOs;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Entities.TicketQuestion;

namespace Ticketing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketQuestionController : ControllerBase
    {
        private readonly ITicketQuestionServices _ticketQuestionServices;
        private readonly ITicketService _ticketService;

        public TicketQuestionController(ITicketQuestionServices ticketQuestionServices, ITicketService ticketService)
        {
            _ticketQuestionServices = ticketQuestionServices;
            _ticketService = ticketService;
        }

        #region Get

        //Get All TicketQuestions
        [HttpGet]
        public async Task<IActionResult> GetTicketQuestions()
        {
            var ticketQuestions = await _ticketQuestionServices.GetTicketQuestionAsync();
            return Ok(ticketQuestions);
        }

        [HttpGet("{id}",Name = "GetTicketQuestionById")]
        public async Task<IActionResult> GetTicketQuestionById(int id)
        {
            var ticketQuestion = await _ticketQuestionServices.GetTicketQuestionByIdAsync(id);

            if (ticketQuestion == null)
                return NotFound();
            
            return Ok(ticketQuestion);
        }


        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> AddTicketQuestion(TicketQuestionDto ticketQuestionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //Check if TicketId in Dto is exist in database or not 
            if (!await _ticketService.IsExistTicket(ticketQuestionDto.ticketId))
                return BadRequest();
            
            //Store values in ticketQuestion instance
            var ticketQuestion = new TicketQuestion()
            {
                Body = ticketQuestionDto.Body,
                ticketId = ticketQuestionDto.ticketId,
                DateTime = DateTime.Now,
                IsResponded = false
            };

            await _ticketQuestionServices.AddTicketQuestionAsync(ticketQuestion);

            return CreatedAtRoute("GetTicketQuestionById", new { id = ticketQuestion.id }, ticketQuestion);
        }

        #endregion

    }
}