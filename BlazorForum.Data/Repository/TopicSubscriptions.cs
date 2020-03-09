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
        private readonly ApplicationDbContext _context;

        public TopicSubscriptions(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopicSubscription>> GetSubscriptionsForTopicAsync(int topicId)
        {
            return await _context.TopicSubscriptions.Where(p => p.ForumTopicId == topicId).ToListAsync();
        }

        public async Task<bool> AddSubscriptionToTopicAsync(TopicSubscription newSubscription)
        {
            var subscriptions = _context.TopicSubscriptions;

            // Make sure the user isn't already subscribed
            var currentSubscription = await subscriptions
                .Where(p => p.ForumTopicId == newSubscription.ForumTopicId && p.Id == newSubscription.Id)
                .FirstOrDefaultAsync();
            if(currentSubscription == null)
            {
                await subscriptions.AddAsync(newSubscription);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveSubscriptionFromTopicAsync(int topicId, string userId)
        {
            var subscriptions = _context.TopicSubscriptions;
            var sub = await subscriptions.Where(p => p.Id == userId && p.ForumTopicId == topicId).FirstOrDefaultAsync();
            if(sub != null)
            {
                subscriptions.Remove(sub);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAllSubscriptionsForUser(string userId)
        {
            var subscriptions = _context.TopicSubscriptions;
            foreach (var subscription in await subscriptions.Where(p => p.Id == userId).ToListAsync())
            {
                subscriptions.Remove(subscription);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
