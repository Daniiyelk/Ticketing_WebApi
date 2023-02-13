using System.ComponentModel.DataAnnotations;

namespace Ticketing.Core.DTOs;

public class TicketQuestionDto
{
    public int ticketId { get; set; }
        
    [Display(Name = "سوال")]
    [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(500)]
    public string Body { get; set; }
    
}