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
        private readonly ApplicationDbContext _context;

        public ForumPosts(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ForumPost>> GetForumPostsAsync(int topicId)
        {
            return await _context.ForumPosts.Where(p => p.ForumTopicId == topicId).ToListAsync();
        }

        public async Task<List<ForumPost>> GetApprovedForumPostsAsync(int topicId)
        {
            return await _context.ForumPosts.Where(p => p.ForumTopicId == topicId && p.IsApproved == true && p.IsDeleted == false).ToListAsync();
        }

        public async Task<ForumPost> GetForumPostAsync(int postId)
        {
            return await _context.ForumPosts.Where(p => p.ForumPostId == postId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddNewPostAsync(ForumPost newPost)
        {
            var posts = _context.ForumPosts;
            await posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePostAsync(ForumPost editedPost)
        {
            var post = await _context.ForumPosts
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
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var posts = _context.ForumPosts;
            var post = await posts.Where(p => p.ForumPostId == postId).FirstOrDefaultAsync();
            posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkUserPostsAsDeletedAsync(string userId)
        {
            var posts = _context.ForumPosts;
            foreach (var post in await posts.Where(p => p.UserId == userId).ToListAsync())
            {
                post.DeleteReason = "Automated on User Delete";
                post.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
