using BlazorForum.Data;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageForumTopics
    {
        Task<List<ForumTopic>> GetAllForumTopicsAsync(int categoryId);
        Task<List<ForumTopic>> GetNewTopicsAsync(int count);
        Task<List<ForumTopic>> GetActiveTopicsAsync(int count);
        Task<List<ForumTopic>> GetForumTopicsAsync();
        Task<List<ForumTopic>> GetForumCatTopicsAsync(int categoryId);
        Task<ForumTopic> GetForumTopicAsync(int topicId);
        Task<int> PostNewTopicAsync(ForumTopic newTopic);
        Task<bool> DeleteForumTopicAsync(int id);
        Task<bool> MarkUserTopicsAsDeletedAsync(string userId);
        Task<bool> UpdateTopicAsync(ForumTopic editedTopic);
        Task<bool> TopicHasAnswerAsync(int topicId);
        Task<bool> IsInSupportForumAsync(int categoryId);
    }

    public class ManageForumTopics : IManageForumTopics
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManageForumTopics(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<ForumTopic>> GetAllForumTopicsAsync(int categoryId) => 
            await new Data.Repository.ForumTopics(_dbFactory).GetAllForumTopicsAsync(categoryId);

        public async Task<List<ForumTopic>> GetNewTopicsAsync(int count) =>
            await new Data.Repository.ForumTopics(_dbFactory).GetNewTopicsAsync(count);

        public async Task<List<ForumTopic>> GetActiveTopicsAsync(int count) =>
            await new Data.Repository.ForumTopics(_dbFactory).GetActiveTopicsAsync(count);

        public async Task<List<ForumTopic>> GetForumTopicsAsync() =>
            await new Data.Repository.ForumTopics(_dbFactory).GetForumTopicsAsync();

        public async Task<List<ForumTopic>> GetForumCatTopicsAsync(int categoryId) =>
            await new Data.Repository.ForumTopics(_dbFactory).GetForumCatTopicsAsync(categoryId);

        public async Task<ForumTopic> GetForumTopicAsync(int topicId) => 
            await new Data.Repository.ForumTopics(_dbFactory).GetForumTopicAsync(topicId);

        public async Task<int> PostNewTopicAsync(ForumTopic newTopic) => 
            await new Data.Repository.ForumTopics(_dbFactory).PostNewTopicAsync(newTopic);

        public async Task<bool> DeleteForumTopicAsync(int id) =>
            await new Data.Repository.ForumTopics(_dbFactory).DeleteForumTopicAsync(id);

        public async Task<bool> MarkUserTopicsAsDeletedAsync(string userId) =>
            await new Data.Repository.ForumTopics(_dbFactory).MarkUserTopicsAsDeletedAsync(userId);

        public async Task<bool> UpdateTopicAsync(ForumTopic editedTopic) =>
            await new Data.Repository.ForumTopics(_dbFactory).UpdateTopicAsync(editedTopic);

        public async Task<bool> TopicHasAnswerAsync(int topicId) =>
            await new Data.Repository.ForumTopics(_dbFactory).TopicHasAnswerAsync(topicId);

        public async Task<bool> IsInSupportForumAsync(int categoryId) =>
            await new Data.Repository.ForumTopics(_dbFactory).IsInSupportForumAsync(categoryId);
    }
}
