using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.DataLayer.Entities.TicketAnswer
{
    public class TicketAnswer
    {
        [Key]
        public int id { get; set; }

        public int adminId { get; set; }

        public int ticketId { get; set; }

        public int? questionId { get; set; }

        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500)]
        public string Body { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        #region Relations

        [ForeignKey("ticketId")]
        public Ticket.Ticket Ticket { get; set; }

        [ForeignKey("adminId")]
        public Admin.Admin Admin { get; set; }

        [ForeignKey("questionId")]
        public TicketQuestion.TicketQuestion TicketQuestion { get; set; }  

        #endregion
    }
}
