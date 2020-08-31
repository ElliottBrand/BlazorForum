using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Data;
using BlazorForum.Models;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageForums
    {
        Task<List<Forum>> GetForumsAsync();
        Task<Forum> GetForumAsync(int id);
        Task<bool> CreateForumAsync(Forum newForum);
        Task<bool> UpdateForumAsync(Forum editedForum);
        Task<bool> DeleteForumAsync(int forumId);
    }

    public class ManageForums : IManageForums
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public ManageForums(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Forum>> GetForumsAsync() => 
            await new Data.Repository.Forums(_dbFactory).GetForumsAsync();

        public async Task<Forum> GetForumAsync(int id) => 
            await new Data.Repository.Forums(_dbFactory).GetForumAsync(id);

        public async Task<bool> CreateForumAsync(Forum newForum) => 
            await new Data.Repository.Forums(_dbFactory).CreateForumAsync(newForum);

        public async Task<bool> UpdateForumAsync(Forum editedForum) =>
            await new Data.Repository.Forums(_dbFactory).UpdateForumAsync(editedForum);

        public async Task<bool> DeleteForumAsync(int forumId) =>
            await new Data.Repository.Forums(_dbFactory).DeleteForumAsync(forumId);
    }
}
