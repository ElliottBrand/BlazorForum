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
        private readonly ApplicationDbContext _context;

        public Forums(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Forum>> GetForumsAsync()
        {
            var forums = await _context.Forums.ToListAsync();

            foreach (var forum in forums)
            {
                forum.ForumCategories = await _context.ForumCategories
                    .Where(p => p.ForumId == forum.ForumId).ToListAsync();
            }

            return forums;
        }

        public async Task<Forum> GetForumAsync(int id)
        {
            return await _context.Forums.Where(p => p.ForumId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateForumAsync(Forum newForum)
        {
            var forums = _context.Forums;
            await forums.AddAsync(newForum);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateForumAsync(Forum editedForum)
        {
            var forum = _context.Forums.Where(p => p.ForumId == editedForum.ForumId).FirstOrDefault();
            forum.Title = editedForum.Title;
            forum.EnableUpDownVotes = editedForum.EnableUpDownVotes;
            forum.Description = editedForum.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteForumAsync(int forumId)
        {
            var forums = _context.Forums;
            var forum = await forums.Where(p => p.ForumId == forumId).FirstOrDefaultAsync();
            if(forum != null)
            {
                forums.Remove(forum);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
