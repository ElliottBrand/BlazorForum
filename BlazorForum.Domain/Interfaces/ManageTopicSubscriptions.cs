using BlazorForum.Data;
using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageTopicSubscriptions
    {
        public Task<List<TopicSubscription>> GetSubscriptionsForTopicAsync(int topicId);
        public Task<bool> AddSubscriptionToTopicAsync(TopicSubscription newSubscription);
        public Task<bool> RemoveSubscriptionFromTopicAsync(int topicId, string userId);
        public Task<bool> DeleteAllSubscriptionsForUser(string userId);
    }

    public class ManageTopicSubscriptions : IManageTopicSubscriptions
    {
        private readonly ApplicationDbContext _context;

        public ManageTopicSubscriptions(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopicSubscription>> GetSubscriptionsForTopicAsync(int topicId) =>
            await new Data.Repository.TopicSubscriptions(_context).GetSubscriptionsForTopicAsync(topicId);

        public async Task<bool> AddSubscriptionToTopicAsync(TopicSubscription newSubscription) =>
            await new Data.Repository.TopicSubscriptions(_context).AddSubscriptionToTopicAsync(newSubscription);

        public async Task<bool> RemoveSubscriptionFromTopicAsync(int topicId, string userId) =>
            await new Data.Repository.TopicSubscriptions(_context).RemoveSubscriptionFromTopicAsync(topicId, userId);

        public async Task<bool> DeleteAllSubscriptionsForUser(string userId) =>
            await new Data.Repository.TopicSubscriptions(_context).DeleteAllSubscriptionsForUser(userId);
    }
}
