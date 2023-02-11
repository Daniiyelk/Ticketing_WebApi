using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.DataLayer.Entities.TicketQuestion
{
    public class TicketQuestion
    {
        [Key]
        public int id { get; set; }
        
        public int ticketId { get; set; }
        
        [Display(Name = "سوال")]
        [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(500)]
        public string Body { get; set; }

        [Display(Name = "تاریخ")]
        [Required (ErrorMessage ="لطفا {0} را وارد کنید")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        [Display(Name ="آیا پاسخ داده شده ؟")]
        public bool IsResponded { get; set; }

        #region Relations

        [ForeignKey("ticketId")]
        public Ticket.Ticket Ticket { get; set; }

        #endregion
    }
}
