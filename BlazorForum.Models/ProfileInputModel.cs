using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Models
{
    public class ProfileInputModel
    {
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [MaxLength(30, ErrorMessage = "Your display name must not be more than 30 characters.")]
        [Display(Name = "Display name")]
        public string DisplayName { get; set; }

        [MaxLength(100, ErrorMessage = "Your title must not be more than 100 characters.")]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Your location must not be more than 100 characters.")]
        public string Location { get; set; }

        [MaxLength(1000, ErrorMessage = "Your about me text must not be more than 1000 characters.")]
        public string About { get; set; }

        [MaxLength(200, ErrorMessage = "Your username or link must not be more than 200 characters.")]
        [Display(Name = "GitHub username or link")]
        public string GitHub { get; set; }

        [MaxLength(200, ErrorMessage = "Your username or link must not be more than 200 characters.")]
        [Display(Name = "LinkedIn username or link")]
        public string LinkedIn { get; set; }

        [MaxLength(200, ErrorMessage = "Your username or link must not be more than 200 characters.")]
        [Display(Name = "Twitter username or link")]
        public string Twitter { get; set; }

        [MaxLength(200, ErrorMessage = "Your link must not be more than 200 characters.")]
        [Display(Name = "Website link")]
        [Url]
        public string Website { get; set; }
    }
}
