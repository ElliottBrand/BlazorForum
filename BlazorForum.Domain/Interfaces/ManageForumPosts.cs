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
        private ApplicationDbContext _context;

        public ManageForumPosts(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ForumPost>> GetForumPostsAsync(int topicId) => 
            await new Data.Repository.ForumPosts(_context).GetForumPostsAsync(topicId);

        public async Task<List<ForumPost>> GetApprovedForumPostsAsync(int topicId) =>
            await new Data.Repository.ForumPosts(_context).GetApprovedForumPostsAsync(topicId);

        public async Task<bool> AddNewPostAsync(ForumPost newPost) => 
            await new Data.Repository.ForumPosts(_context).AddNewPostAsync(newPost);

        public async Task<bool> DeletePostAsync(int postId) =>
            await new Data.Repository.ForumPosts(_context).DeletePostAsync(postId);

        public async Task<bool> MarkUserPostsAsDeletedAsync(string userId) =>
            await new Data.Repository.ForumPosts(_context).MarkUserPostsAsDeletedAsync(userId);

        public async Task<ForumPost> GetForumPostAsync(int postId) =>
            await new Data.Repository.ForumPosts(_context).GetForumPostAsync(postId);

        public async Task<bool> UpdatePostAsync(ForumPost editedPost) =>
            await new Data.Repository.ForumPosts(_context).UpdatePostAsync(editedPost);
    }
}
