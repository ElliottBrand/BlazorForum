using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Domain.Interfaces;

namespace BlazorForum.Domain.Helpers.Forum
{
    public class ForumTopicHelpers
    {
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;

        public ForumTopicHelpers(IManageTopicSubscriptions manageTopicSubscriptions)
        {
            _manageTopicSubscriptions = manageTopicSubscriptions;
        }

        public async Task<bool> CurrentUserIsSubscribedToTopic(string currentUserId, int topicId)
        {
            if(currentUserId != null)
            {
                var subscriptions = await _manageTopicSubscriptions.GetSubscriptionsForTopicAsync(topicId);
                var subscription = subscriptions.Where(p => p.Id == currentUserId).FirstOrDefault();
                return subscription == null ? false : true;
            }
            return false;
        }
    }
}
