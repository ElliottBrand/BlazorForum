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

        /// <summary>
        /// Returns a list of topic post counts, where the post count is associated with the parent topic Id.
        /// </summary>
        /// <param name="forumTopics"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Selects the topic based on the ForumTopicId, then returns the post count
        /// </summary>
        /// <param name="topicPostCountList"></param>
        /// <param name="forumTopicId"></param>
        /// <returns></returns>
        public static int GetTopicCount(List<TopicPostCount> topicPostCountList, int forumTopicId)
        {
            return topicPostCountList != null && topicPostCountList.Count != 0 ? topicPostCountList.Where(p => p.ParentItemId == forumTopicId)
                .FirstOrDefault().ChildCount : 0;
        }
    }
}
