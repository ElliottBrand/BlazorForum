using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class Forums
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public Forums(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Forum>> GetForumsAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var forums = await context.Forums.ToListAsync();

            foreach (var forum in forums)
            {
                forum.ForumCategories = await context.ForumCategories
                    .Where(p => p.ForumId == forum.ForumId).ToListAsync();
            }

            return forums;
        }

        public async Task<Forum> GetForumAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Forums.Where(p => p.ForumId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateForumAsync(Forum newForum)
        {
            using var context = _dbFactory.CreateDbContext();
            var forums = context.Forums;
            await forums.AddAsync(newForum);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateForumAsync(Forum editedForum)
        {
            using var context = _dbFactory.CreateDbContext();
            var forum = context.Forums.Where(p => p.ForumId == editedForum.ForumId).FirstOrDefault();
            forum.Title = editedForum.Title;
            forum.EnableUpDownVotes = editedForum.EnableUpDownVotes;
            forum.IsSupportForum = editedForum.IsSupportForum;
            forum.Description = editedForum.Description;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteForumAsync(int forumId)
        {
            using var context = _dbFactory.CreateDbContext();
            var forums = context.Forums;
            var forum = await forums.Where(p => p.ForumId == forumId).FirstOrDefaultAsync();
            if(forum != null)
            {
                forums.Remove(forum);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if any forums are set as support forums.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SupportForumExistsAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var forums = await context.Forums.ToListAsync();
            foreach (var forum in forums)
            {
                if (forum.IsSupportForum == true)
                    return true;
            }
            return false;
        }
    }
}
