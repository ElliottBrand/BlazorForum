using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorForum.Models
{
    public class UpDownVote
    {
        public int UpDownVoteId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [MaxLength(256)]
        public string UniqueIdentifier { get; set; }

        [Required]
        [MaxLength(256)]
        public string PosterId { get; set; }

        [Required]
        [MaxLength(256)]
        public string VoterId { get; set; }

        public DateTime DateVoted { get; set; }

        [Required]
        public int VoteIncrement { get; set; }
    }
}
