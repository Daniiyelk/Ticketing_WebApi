using System.ComponentModel.DataAnnotations;

namespace Ticketing.Core.DTOs;

public class AdminDto
{
    [Display(Name = "ایمیل")]
    [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
    [MaxLength(75)]
    public string? Email { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50)]
    public string? Password { get; set; }
}