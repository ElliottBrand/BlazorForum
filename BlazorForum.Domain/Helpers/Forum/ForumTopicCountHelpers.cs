using BlazorForum.Domain.Interfaces;
using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Helpers.Forum
{
    public class ForumTopicCountHelpers
    {
        private readonly IManageForumPosts _manageForumPosts;

        public ForumTopicCountHelpers(IManageForumPosts manageForumPosts)
        {
            _manageForumPosts = manageForumPosts;
        }

        public async Task<List<TopicPostCount>> GetTopicsPostCountListAsync(List<ForumTopic> forumTopics)
        {
            var postCountList = new List<TopicPostCount>();

            foreach (var topic in forumTopics)
            {
                var posts = await _manageForumPosts.GetApprovedForumPostsAsync(topic.ForumTopicId);
                var postCount = new TopicPostCount
                {
                    ParentItemId = topic.ForumTopicId,
                    ChildCount = posts.Count
                };
                postCountList.Add(postCount);
            }
            return postCountList;
        }

        public static int GetTopicCount(List<TopicPostCount> topicPostCountList, int forumTopicId)
        {
            return topicPostCountList != null ? topicPostCountList.Where(p => p.ParentItemId == forumTopicId)
                .FirstOrDefault().ChildCount : 0;
        }
    }
}
