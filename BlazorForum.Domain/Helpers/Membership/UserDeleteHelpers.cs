using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Domain.Interfaces;

namespace BlazorForum.Domain.Helpers.Membership
{
    public class UserDeleteHelpers
    {
        private readonly IManageForumTopics _manageForumTopics;
        private readonly IManageForumPosts _manageForumPosts;
        private readonly IManageUpDownVotes _manageUpDownVotes;
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;

        public UserDeleteHelpers(IManageForumTopics manageForumTopics, IManageForumPosts manageForumPosts, 
            IManageUpDownVotes manageUpDownVotes, IManageTopicSubscriptions manageTopicSubscriptions)
        {
            _manageForumTopics = manageForumTopics;
            _manageForumPosts = manageForumPosts;
            _manageUpDownVotes = manageUpDownVotes;
            _manageTopicSubscriptions = manageTopicSubscriptions;
        }

        /// <summary>
        /// This method runs to clean up topics, posts, and various data that could break the site after deleting the user.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CleanupUserData(string userId)
        {
            await _manageForumTopics.MarkUserTopicsAsDeletedAsync(userId);

            await _manageForumPosts.MarkUserPostsAsDeletedAsync(userId);

            await _manageUpDownVotes.DeleteUpDownVotesByUserAsync(userId);

            await _manageUpDownVotes.DeleteUpDownVotesForUserAsync(userId);

            await _manageTopicSubscriptions.DeleteAllSubscriptionsForUser(userId);

            return true;
        }


    }
}
