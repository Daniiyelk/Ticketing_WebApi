using System.ComponentModel.DataAnnotations;

namespace Ticketing.Core.DTOs;

public class TicketAnswerDto
{
    public int adminId { get; set; }

    public int ticketId { get; set; }

    public int questionId { get; set; }

    [Display(Name = "پاسخ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500)]
    public string Body { get; set; }
}