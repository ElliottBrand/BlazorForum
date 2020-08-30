using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class ForumTopics
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ForumTopics(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<ForumTopic>> GetAllForumTopicsAsync(int catId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumTopics.Where(p => p.ForumCategoryId == catId).ToListAsync();
        }

        public async Task<List<ForumTopic>> GetNewTopicsAsync(int count)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumTopics.Where(p => p.IsApproved == true && p.IsDeleted == false)
                .OrderByDescending(p => p.PostedDate).Take(count).ToListAsync();
        }

        public async Task<List<ForumTopic>> GetActiveTopicsAsync(int count)
        {
            using var context = _dbFactory.CreateDbContext();
            var topics = await context.ForumTopics.Where(p => p.IsApproved == true && p.IsDeleted == false).ToListAsync();
            foreach(var topic in topics)
            {
                topic.ForumPosts = await context.ForumPosts.Where(p => p.ForumTopicId == topic.ForumTopicId && p.IsApproved == true && p.IsDeleted == false).ToListAsync();
            }

            return topics.Where(p => p.ForumPosts.Count > 0)
                .OrderByDescending(p => p.ForumPosts.OrderByDescending(x => x.PostedDate)
                .FirstOrDefault().PostedDate).Take(count).ToList();
        }

        public async Task<List<ForumTopic>> GetForumTopicsAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumTopics.Where(p => p.IsApproved == true && p.IsDeleted == false).ToListAsync();
        }

        public async Task<List<ForumTopic>> GetForumCatTopicsAsync(int catId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumTopics.Where(p => p.ForumCategoryId == catId && p.IsApproved == true && p.IsDeleted == false).ToListAsync();
        }

        public async Task<ForumTopic> GetForumTopicAsync(int topicId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumTopics.Where(p => p.ForumTopicId == topicId).FirstOrDefaultAsync();
        }

        public async Task<int> PostNewTopicAsync(ForumTopic newTopic)
        {
            using var context = _dbFactory.CreateDbContext();
            var topics = context.ForumTopics;
            await topics.AddAsync(newTopic);
            await context.SaveChangesAsync();
            return newTopic.ForumTopicId;
        }

        public async Task<bool> UpdateTopicAsync(ForumTopic editedTopic)
        {
            using var context = _dbFactory.CreateDbContext();
            var topic = await context.ForumTopics
                .Where(p => p.ForumTopicId == editedTopic.ForumTopicId).FirstOrDefaultAsync();
            if(topic != null)
            {
                topic.ForumCategoryId = editedTopic.ForumCategoryId;
                topic.Title = editedTopic.Title;
                topic.TopicText = editedTopic.TopicText;
                topic.IsApproved = editedTopic.IsApproved;
                topic.Flags = editedTopic.Flags;
                topic.IsModeratorChanged = editedTopic.IsModeratorChanged;
                topic.EditedDate = editedTopic.EditedDate;
                topic.EditedBy = editedTopic.EditedBy;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteForumTopicAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var topics = context.ForumTopics;
            var topic = await topics.Where(p => p.ForumTopicId == id).FirstOrDefaultAsync();
            var removed = topics.Remove(topic);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkUserTopicsAsDeletedAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var topics = context.ForumTopics;
            foreach (var topic in await topics.Where(p => p.UserId == userId).ToListAsync())
            {
                topic.DeleteReason = "Automated on User Delete";
                topic.IsDeleted = true;
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
