using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Core.DTOs;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Entities.TicketAnswer;

namespace Ticketing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketAnswerController : ControllerBase
    {
        private readonly ITicketAnswerServices _ticketAnswerServices;
        private readonly ITicketQuestionServices _ticketQuestionServices;
        private readonly ITicketService _ticketService;
        private readonly IAdminServices _adminServices;
        private readonly IMapper _mapper;
        
        public TicketAnswerController(ITicketAnswerServices ticketAnswerServices, ITicketQuestionServices ticketQuestionServices, ITicketService ticketService, IAdminServices adminServices, IMapper mapper)
        {
            _ticketAnswerServices = ticketAnswerServices;
            _ticketQuestionServices = ticketQuestionServices;
            _ticketService = ticketService;
            _adminServices = adminServices;
            _mapper = mapper;
        }

        #region Get

        //Get all ticket answers
        [HttpGet]
        public async Task<IActionResult> GetAllTicketAnswers()
        {
            var ticketAsnwers = await _ticketAnswerServices.GetAllTicketAnswersAsync();
            return Ok(ticketAsnwers);
        }
        
        //Get Specified ticketAnswer
        [HttpGet("{id}",Name = "GetTicketAnswerById")]
        public async Task<IActionResult> GetTicketAnswerById(int id)
        {
            var ticketAnswer = await _ticketAnswerServices.GetTicketAnswerByIdAsync(id);

            if (ticketAnswer == null)
                return NotFound();

            return Ok(ticketAnswer);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> AddTicketAnswer(TicketAnswerDto ticketAnswerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            //Checking is AdminId Exist Or Not
            if (!await _adminServices.IsExistAdmin(ticketAnswerDto.adminId))
                return BadRequest();
            
            //Checking is TicketId Exist Or Not
            if (!await _ticketService.IsExistTicket(ticketAnswerDto.ticketId))
                return BadRequest();

            //Checking is QuestionId Exist Or Not
            if (!await _ticketQuestionServices.IsExistTicketQuestion(ticketAnswerDto.questionId))
                return BadRequest();

            var ticketAnswer= _mapper.Map<TicketAnswer>(ticketAnswerDto);
            ticketAnswer.DateTime = DateTime.Now;

            await _ticketAnswerServices.AddTicketAnswerAsync(ticketAnswer);
            
            //Todo true the isFinished of ticketQuestion
            
            return CreatedAtRoute("GetTicketAnswerById", new { id = ticketAnswer.id }, ticketAnswer);
        }

        #endregion
        
    }
}