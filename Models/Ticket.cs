using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShadowTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Description")]
        [StringLength(2500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated")]
        public DateTimeOffset? Updated { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }
        [DisplayName("Archived by Project")]
        public bool ArchivedByProject { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; }
        [DisplayName("Ticket Type")]
        public int TicketTypeId { get; set; }
        [DisplayName("Ticket Status")]
        public int TicketStatusId { get; set; }
        [DisplayName("Ticket Priority")]
        public int TicketPriorityId { get; set; }
        [DisplayName("Ticket Owner")]
        public string OwnerUserId { get; set; }
        [DisplayName("Ticket Developer")]
        public string DeveloperUserId { get; set; }





        //Nav Props
        public virtual BTUser OwnerUser { get; set; }
        public virtual BTUser DeveloperUser { get; set; }
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        public ICollection<TicketComment> Comments = new HashSet<TicketComment>();
        public ICollection<TicketAttachment> Attachments = new HashSet<TicketAttachment>();
        public ICollection<TicketHistory> History = new HashSet<TicketHistory>();
        public ICollection<Notification> Notifications = new HashSet<Notification>();
        public ICollection<TicketTask> Tasks = new HashSet<TicketTask>();






    }
}
