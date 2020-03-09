using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorForum.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string AnalyticsCode { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [MaxLength(100)]
        public string EmailSenderName { get; set; }

        [MaxLength(255)]
        public string SendGridKey { get; set; }

        public string RegistrationApprovalMessage { get; set; }

    }
}
