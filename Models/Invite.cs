﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShadowTracker.Models
{
    public class Invite
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Sent")]
        public DateTimeOffset InviteDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Joined")]
        public DateTimeOffset JoinDate { get; set; }

        [DisplayName("Code")]
        public Guid CompanyToken { get; set; }

        [DisplayName("Company")]
        public int CompanyId { get; set; }
        [DisplayName("Project")]
        public int ProjectId { get; set; }
        [DisplayName("Invitor")]
        public string InvitorId { get; set; }
        [DisplayName("Invitee")]
        public string InviteeEmail { get; set; }
        [DisplayName("Invitee First Name")]
        public string InviteeFirstName { get; set; }
        [DisplayName("Invitee Last Name")]
        public string InviteeLastName { get; set; }
        [DisplayName("Invitee Message")]
        public string Message { get; set; }
        [DisplayName("Is Valid")]
        public bool IsValid { get; set; }



        // Navigaiton Props
        public virtual Company Company { get; set; }
        public virtual BTUser Invitor { get; set; }
        public virtual BTUser Invitee { get; set; }
        public virtual Project Project { get; set; }




    }
}
