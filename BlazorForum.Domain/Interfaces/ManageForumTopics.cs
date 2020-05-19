using BlazorForum.Data;
using BlazorForum.Models;
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
    }

    public class ManageForumTopics : IManageForumTopics
    {
        private readonly ApplicationDbContext _context;

        public ManageForumTopics(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ForumTopic>> GetAllForumTopicsAsync(int categoryId) => 
            await new Data.Repository.ForumTopics(_context).GetAllForumTopicsAsync(categoryId);

        public async Task<List<ForumTopic>> GetNewTopicsAsync(int count) =>
            await new Data.Repository.ForumTopics(_context).GetNewTopicsAsync(count);

        public async Task<List<ForumTopic>> GetActiveTopicsAsync(int count) =>
            await new Data.Repository.ForumTopics(_context).GetActiveTopicsAsync(count);

        public async Task<List<ForumTopic>> GetForumTopicsAsync() =>
            await new Data.Repository.ForumTopics(_context).GetForumTopicsAsync();

        public async Task<List<ForumTopic>> GetForumCatTopicsAsync(int categoryId) =>
            await new Data.Repository.ForumTopics(_context).GetForumCatTopicsAsync(categoryId);

        public async Task<ForumTopic> GetForumTopicAsync(int topicId) => 
            await new Data.Repository.ForumTopics(_context).GetForumTopicAsync(topicId);

        public async Task<int> PostNewTopicAsync(ForumTopic newTopic) => 
            await new Data.Repository.ForumTopics(_context).PostNewTopicAsync(newTopic);

        public async Task<bool> DeleteForumTopicAsync(int id) =>
            await new Data.Repository.ForumTopics(_context).DeleteForumTopicAsync(id);

        public async Task<bool> MarkUserTopicsAsDeletedAsync(string userId) =>
            await new Data.Repository.ForumTopics(_context).MarkUserTopicsAsDeletedAsync(userId);

        public async Task<bool> UpdateTopicAsync(ForumTopic editedTopic) =>
            await new Data.Repository.ForumTopics(_context).UpdateTopicAsync(editedTopic);
    }
}
