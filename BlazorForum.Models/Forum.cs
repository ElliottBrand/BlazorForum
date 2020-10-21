using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorForum.Models
{
    public class Forum
    {
        public int ForumId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public bool EnableUpDownVotes { get; set; }

        public bool IsSupportForum { get; set; }

        public virtual ICollection<ForumCategory> ForumCategories { get; set; }
    }
}
