using System.ComponentModel.DataAnnotations;

namespace Ticketing.Core.DTOs;

public class EmailDto
{
    [Display(Name = "موضوع")]
    [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(150)]
    public string? Subject { get; set; }
    
    [Display(Name = "متن اصلی")]
    [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(500)]
    public string? Body { get; set; }
    
    [Display(Name = "ارسال برای")]
    [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(150)]
    public string? MailTo { get; set; }
    
}