using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ShadowTracker.Models
{
    public class Project
    {



        public int Id { get; set; }

        [DisplayName("Company Id")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DisplayName("Project Name")]
        public string Name { get; set; }

        [StringLength(2500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTimeOffset? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTimeOffset? EndDate { get; set; }

        [DisplayName("Priority")]
        public int? ProjectPriorityId { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }



        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Project Image")]
        public IFormFile ProjectImageFile { get; set; } // file on computer 
        public byte[] ProjectImageData { get; set; } // file split up into bytes and added into an array // byte[] ImageData
        public string ProjectImageType { get; set; } // store the image type so it can be rendered


        // Nav Props
        public virtual Company Company { get; set; }
        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();


    }
}
