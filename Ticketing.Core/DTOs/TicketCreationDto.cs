using System.ComponentModel.DataAnnotations;

namespace Ticketing.Core.DTOs;

public class TicketCreationDto
{
    [Required]
    public int userId { get; set; }

    [Display(Name = "موضوع")]
    [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(150)]
    public string Title { get; set; }

    [Display(Name = "خاتمه یافت ؟")] 
    public bool IsFinished { get; set; } = false;
}