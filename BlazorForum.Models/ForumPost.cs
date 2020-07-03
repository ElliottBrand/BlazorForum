using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazorForum.Models
{
    public class ForumPost
    {
        public int ForumPostId { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        [Required]
        public string PostText { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [NotMapped]
        [MaxLength(256)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string UserIP { get; set; }

        [Required]
        public int Flags { get; set; }

        public DateTime? EditedDate { get; set; }

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
        public int ForumTopicId { get; set; }

        public int? ParentPostId { get; set; }
    }
}
