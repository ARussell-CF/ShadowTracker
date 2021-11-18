using System;
using System.ComponentModel.DataAnnotations;

namespace ShadowTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }

        // these needed to be nullable so do not at Required

        [Display(Name = "Updated Item")]
        public string Property { get; set; }

        [Display(Name = "Previous")]
        public string OldValue { get; set; }

        [Display(Name = "Current")]
        public string NewValue { get; set; }


        [Display(Name = "Date Modified")]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "Description of Change")]
        public string Description { get; set; }

        /// <summary>
        ///  end
        /// </summary>




        [Display(Name = "Ticket")]
        public int TicketId { get; set; }

        [Required]
        [Display(Name = "Team Member")]
        public string UserId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual BTUser User { get; set; }

    }
}
