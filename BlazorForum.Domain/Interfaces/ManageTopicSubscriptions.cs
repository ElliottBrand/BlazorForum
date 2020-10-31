using BlazorForum.Data;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageTopicSubscriptions
    {
        public Task<List<TopicSubscription>> GetSubscriptionsForTopicAsync(int topicId);
        public Task<bool> SubscribeUserToTopicAsync(TopicSubscription newSubscription);
        public Task<bool> RemoveSubscriptionFromTopicAsync(int topicId, string userId);
        public Task<bool> DeleteAllSubscriptionsForUser(string userId);
    }

    public class ManageTopicSubscriptions : IManageTopicSubscriptions
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManageTopicSubscriptions(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<TopicSubscription>> GetSubscriptionsForTopicAsync(int topicId) =>
            await new Data.Repository.TopicSubscriptions(_dbFactory).GetSubscriptionsForTopicAsync(topicId);

        public async Task<bool> SubscribeUserToTopicAsync(TopicSubscription newSubscription) =>
            await new Data.Repository.TopicSubscriptions(_dbFactory).SubscribeUserToTopicAsync(newSubscription);

        public async Task<bool> RemoveSubscriptionFromTopicAsync(int topicId, string userId) =>
            await new Data.Repository.TopicSubscriptions(_dbFactory).RemoveSubscriptionFromTopicAsync(topicId, userId);

        public async Task<bool> DeleteAllSubscriptionsForUser(string userId) =>
            await new Data.Repository.TopicSubscriptions(_dbFactory).DeleteAllSubscriptionsForUser(userId);
    }
}
