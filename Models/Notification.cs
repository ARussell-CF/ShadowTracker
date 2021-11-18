using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShadowTracker.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [StringLength(3500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DisplayName("Message")]
        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Viewed Date")]
        public DateTimeOffset? Viewed { get; set; }

        [DisplayName("Viewed")]
        public bool IsViewed { get; set; }

        [DisplayName("Ticket")]
        public int? TicketId { get; set; }

        [DisplayName("Project")]
        public int? ProjectId { get; set; }

        [Required]
        [DisplayName("Recipients")]
        public string RecipientId { get; set; }
        [Required]
        [DisplayName("Sender")]
        public string SenderId { get; set; }


        //Navigational Properties -- Lookup Table

        public virtual NotificationType NotificationType { get; set; }
        public virtual Project Project { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual BTUser Recipient { get; set; }
        public virtual BTUser Sender { get; set; }


    }
}

