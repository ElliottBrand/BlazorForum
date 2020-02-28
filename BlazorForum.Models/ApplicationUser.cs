using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BlazorForum.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(256)]
        public override string UserName { get; set; }

        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public override string Email { get; set; }

        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$",
            ErrorMessage = "The phone number appears to be formatted improperly.")]
        public override string PhoneNumber { get; set; }
    }
}
