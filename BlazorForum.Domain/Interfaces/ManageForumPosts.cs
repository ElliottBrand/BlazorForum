using BlazorForum.Data;
using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageForumPosts
    {
        Task<List<ForumPost>> GetForumPostsAsync(int topicId);
        Task<List<ForumPost>> GetApprovedForumPostsAsync(int topicId);
        Task<bool> AddNewPostAsync(ForumPost newPost);
        Task<bool> DeletePostAsync(int postId);
        Task<bool> MarkUserPostsAsDeletedAsync(string userId);
        Task<ForumPost> GetForumPostAsync(int postId);
        Task<bool> UpdatePostAsync(ForumPost editedPost);
    }

    public class ManageForumPosts : IManageForumPosts
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManageForumPosts(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<ForumPost>> GetForumPostsAsync(int topicId) => 
            await new Data.Repository.ForumPosts(_dbFactory).GetForumPostsAsync(topicId);

        public async Task<List<ForumPost>> GetApprovedForumPostsAsync(int topicId) =>
            await new Data.Repository.ForumPosts(_dbFactory).GetApprovedForumPostsAsync(topicId);

        public async Task<bool> AddNewPostAsync(ForumPost newPost) => 
            await new Data.Repository.ForumPosts(_dbFactory).AddNewPostAsync(newPost);

        public async Task<bool> DeletePostAsync(int postId) =>
            await new Data.Repository.ForumPosts(_dbFactory).DeletePostAsync(postId);

        public async Task<bool> MarkUserPostsAsDeletedAsync(string userId) =>
            await new Data.Repository.ForumPosts(_dbFactory).MarkUserPostsAsDeletedAsync(userId);

        public async Task<ForumPost> GetForumPostAsync(int postId) =>
            await new Data.Repository.ForumPosts(_dbFactory).GetForumPostAsync(postId);

        public async Task<bool> UpdatePostAsync(ForumPost editedPost) =>
            await new Data.Repository.ForumPosts(_dbFactory).UpdatePostAsync(editedPost);
    }
}
