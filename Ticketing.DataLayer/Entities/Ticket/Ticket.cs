using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.DataLayer.Entities.Ticket
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int userId { get; set; }

        [Display(Name = "موضوع")]
        [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string Title { get; set; }

        [Display(Name = "تاریخ")]
        [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        [Display(Name = "خاتمه یافت ؟")]
        public bool IsFinished { get; set; }

        #region Relations

        [ForeignKey("userId")]
        public User.User User { get; set; }

        public List<TicketQuestion.TicketQuestion> TicketQuestions { get; set; }
        public List<TicketAnswer.TicketAnswer> TicketAnswers { get; set; }

        #endregion
    }
}
