using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorForum.Models
{
    public class ForumTopic
    {
        public int ForumTopicId { get; set; }

        [Required]
        [MaxLength(125)]
        public string Title { get; set; }

        [Required]
        public string TopicText { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [NotMapped]
        [MaxLength(256)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string UserIP { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        public DateTime? EditedDate { get; set; }

        [Required]
        public int Flags { get; set; }

        [MaxLength(100)]
        public string EditReason { get; set; }

        [MaxLength(450)]
        public string EditedBy { get; set; }

        [Required]
        public bool IsModeratorChanged { get; set; }

        [MaxLength(100)]
        public string DeleteReason { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public int ForumCategoryId { get; set; }

        public virtual ICollection<ForumPost> ForumPosts { get; set; }
    }
}
