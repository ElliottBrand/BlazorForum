using BlazorForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class ForumPosts
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ForumPosts(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<ForumPost>> GetForumPostsAsync(int topicId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumPosts.Where(p => p.ForumTopicId == topicId).ToListAsync();
        }

        public async Task<List<ForumPost>> GetApprovedForumPostsAsync(int topicId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumPosts.Where(p => p.ForumTopicId == topicId && p.IsApproved == true && p.IsDeleted == false).ToListAsync();
        }

        public async Task<ForumPost> GetForumPostAsync(int postId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumPosts.Where(p => p.ForumPostId == postId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddNewPostAsync(ForumPost newPost)
        {
            using var context = _dbFactory.CreateDbContext();
            var posts = context.ForumPosts;
            await posts.AddAsync(newPost);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePostAsync(ForumPost editedPost)
        {
            using var context = _dbFactory.CreateDbContext();
            var post = await context.ForumPosts
                .Where(p => p.ForumPostId == editedPost.ForumPostId).FirstOrDefaultAsync();
            if(post != null)
            {
                post.PostText = editedPost.PostText;
                post.IsApproved = editedPost.IsApproved;
                post.Flags = editedPost.Flags;
                post.IsModeratorChanged = editedPost.IsModeratorChanged;
                post.EditedBy = editedPost.EditedBy;
                post.EditedDate = editedPost.EditedDate;
                post.EditReason = editedPost.EditReason;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            using var context = _dbFactory.CreateDbContext();
            var posts = context.ForumPosts;
            var post = await posts.Where(p => p.ForumPostId == postId).FirstOrDefaultAsync();
            posts.Remove(post);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetPostAnswerStatusAsync(int postId, bool isAnswer)
        {
            using var context = _dbFactory.CreateDbContext();
            var post = await context.ForumPosts.Where(p => p.ForumPostId == postId).FirstOrDefaultAsync();
            if(post != null)
            {
                post.IsAnswer = isAnswer;
                await context.SaveChangesAsync();
                return isAnswer;
            }
            return !isAnswer;
        }

        public async Task<bool> MarkUserPostsAsDeletedAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var posts = context.ForumPosts;
            foreach (var post in await posts.Where(p => p.UserId == userId).ToListAsync())
            {
                post.DeleteReason = "Automated on User Delete";
                post.IsDeleted = true;
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
