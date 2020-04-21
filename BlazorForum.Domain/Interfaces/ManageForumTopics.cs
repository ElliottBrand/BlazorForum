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
        Task<List<ForumTopic>> GetForumTopicsAsync(int categoryId);
        Task<List<ForumTopic>> GetAllApprovedForumTopicsAsync();
        Task<List<ForumTopic>> GetApprovedForumTopicsAsync(int categoryId);
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

        public async Task<List<ForumTopic>> GetForumTopicsAsync(int categoryId) => 
            await new Data.Repository.ForumTopics(_context).GetForumTopicsAsync(categoryId);

        public async Task<List<ForumTopic>> GetAllApprovedForumTopicsAsync() =>
            await new Data.Repository.ForumTopics(_context).GetAllApprovedForumTopicsAsync();

        public async Task<List<ForumTopic>> GetApprovedForumTopicsAsync(int categoryId) =>
            await new Data.Repository.ForumTopics(_context).GetApprovedForumTopicsAsync(categoryId);

        public async Task<ForumTopic> GetForumTopicAsync(int topicId) => 
            await new Data.Repository.ForumTopics(_context).GetForumTopic(topicId);

        public async Task<int> PostNewTopicAsync(ForumTopic newTopic) => 
            await new Data.Repository.ForumTopics(_context).PostNewTopicAsync(newTopic);

        public async Task<bool> DeleteForumTopicAsync(int id) =>
            await new Data.Repository.ForumTopics(_context).DeleteTopicAsync(id);

        public async Task<bool> MarkUserTopicsAsDeletedAsync(string userId) =>
            await new Data.Repository.ForumTopics(_context).MarkUserTopicsAsDeletedAsync(userId);

        public async Task<bool> UpdateTopicAsync(ForumTopic editedTopic) =>
            await new Data.Repository.ForumTopics(_context).UpdateTopicAsync(editedTopic);
    }
}
