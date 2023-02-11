using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.DataLayer.Entities.User
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(75)]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        public string Password { get; set; }

        #region Relations

        public List<Ticket.Ticket> Ticket { get; set; }

        #endregion
    }
}
