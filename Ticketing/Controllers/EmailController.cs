using Microsoft.AspNetCore.Mvc;
using Ticketing.Core.DTOs;
using Ticketing.Core.Services.Interfaces;

namespace Ticketing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailServices _mailServices;

        public EmailController(IMailServices mailServices)
        {
            _mailServices = mailServices;
        }


        #region Post

        [HttpPost]
        public IActionResult SendEmail(EmailDto emailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _mailServices.SendMail(emailDto.Subject, emailDto.Body, emailDto.MailTo);

            return Ok(result);
        }

        #endregion
    }    
}
