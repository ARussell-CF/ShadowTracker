using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShadowTracker.Models
{
    public class ProjectPriority
    {
        public int Id { get; set; }

        [DisplayName("Project Priority")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
