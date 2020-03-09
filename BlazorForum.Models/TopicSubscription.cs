using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorForum.Models
{
    public class TopicSubscription
    {
        [Key]
        public int TopicSubscriptionsId { get; set; }

        // This is the User Id
        [MaxLength(256)]
        public string Id { get; set; }

        public int ForumTopicId { get; set; }
    }
}
