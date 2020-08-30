using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorForum.Data.Repository
{
    public class TopicSubscriptions
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public TopicSubscriptions(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<TopicSubscription>> GetSubscriptionsForTopicAsync(int topicId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.TopicSubscriptions.Where(p => p.ForumTopicId == topicId).ToListAsync();
        }

        public async Task<bool> AddSubscriptionToTopicAsync(TopicSubscription newSubscription)
        {
            using var context = _dbFactory.CreateDbContext();
            var subscriptions = context.TopicSubscriptions;

            // Make sure the user isn't already subscribed
            var currentSubscription = await subscriptions
                .Where(p => p.ForumTopicId == newSubscription.ForumTopicId && p.Id == newSubscription.Id)
                .FirstOrDefaultAsync();
            if(currentSubscription == null)
            {
                await subscriptions.AddAsync(newSubscription);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveSubscriptionFromTopicAsync(int topicId, string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var subscriptions = context.TopicSubscriptions;
            var sub = await subscriptions.Where(p => p.Id == userId && p.ForumTopicId == topicId).FirstOrDefaultAsync();
            if(sub != null)
            {
                subscriptions.Remove(sub);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAllSubscriptionsForUser(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var subscriptions = context.TopicSubscriptions;
            foreach (var subscription in await subscriptions.Where(p => p.Id == userId).ToListAsync())
            {
                subscriptions.Remove(subscription);
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
