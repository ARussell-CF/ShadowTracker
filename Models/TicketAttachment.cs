using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ShadowTracker.Models
{
    public class TicketAttachment
    {

        public int Id { get; set; }

        [Display(Name = "Ticket ID")]
        public int TicketId { get; set; }

        [Display(Name = "Ticket Task")]
        public int? TicketTaskId { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile AttachmentFile { get; set; }
        public byte[] AttachmentData { get; set; }
        public string AttachmentType { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }


        public virtual Ticket Ticket { get; set; }
        public virtual BTUser User { get; set; }





    }
}
