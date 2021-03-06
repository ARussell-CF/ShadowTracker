using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShadowTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }

        [DisplayName("Member Comment")]
        public string Comment { get; set; }

        [DisplayName("Comment Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }


        [DisplayName("Ticket")]
        public int TicketId { get; set; }

        [Required]
        [DisplayName("Team Member")]
        public string UserId { get; set; }


        public virtual BTUser User { get; set; } // a Comment can only have one user
        public virtual Ticket Ticket { get; set; } // a comment can only belong to one ticket 

    }
}
