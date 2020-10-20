using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Data;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManagePages
    {
        Task<SitePage> GetIndexPageAsync();
        Task<List<SitePage>> GetPagesAsync();
        Task<SitePage> GetPageAsync(int pageId);
        Task<bool> UpdatePageAsync(SitePage editedPage);
    }

    public class ManagePages : IManagePages
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManagePages(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<SitePage> GetIndexPageAsync() => 
            await new Data.Repository.SitePages(_dbFactory).GetIndexPageAsync();

        public async Task<List<SitePage>> GetPagesAsync() =>
            await new Data.Repository.SitePages(_dbFactory).GetPagesAsync();

        public async Task<SitePage> GetPageAsync(int pageId) =>
            await new Data.Repository.SitePages(_dbFactory).GetPageAsync(pageId);

        public async Task<bool> UpdatePageAsync(SitePage editedPage) =>
            await new Data.Repository.SitePages(_dbFactory).UpdatePageAsync(editedPage);
    }
}
