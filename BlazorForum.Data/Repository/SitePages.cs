using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorForum.Models;

namespace BlazorForum.Data.Repository
{
    public class SitePages
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public SitePages(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<SitePage> GetIndexPageAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            await CreatePageIfNoneFound();
            return await context.Pages.Where(p => p.IsIndex == true).FirstOrDefaultAsync();
        }

        public async Task<SitePage> GetPageAsync(int pageId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Pages.Where(p => p.SitePageId == pageId).FirstOrDefaultAsync();
        }

        public async Task<List<SitePage>> GetPagesAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Pages.ToListAsync();
        }

        public async Task<bool> UpdatePageAsync(SitePage editedPage)
        {
            using var context = _dbFactory.CreateDbContext();
            var page = await context.Pages.Where(p => p.SitePageId == editedPage.SitePageId).FirstOrDefaultAsync();
            if(page != null)
            {
                page.Title = editedPage.Title;
                page.MainContent = editedPage.MainContent;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task CreatePageIfNoneFound()
        {
            // This is mostly used for the very first page visit after a fresh install.
            using var context = _dbFactory.CreateDbContext();
            var pages = await context.Pages.ToListAsync();
            if(pages.Count == 0)
            {
                var page = new SitePage
                {
                    Title = "Welcome",
                    MainContent = "<p>Welcome to my new BlazorForum website! BlazorForum is an open source " +
                    "forum app built in Blazor. The code can be found on the GitHub repository " +
                    "at <a href=\"https://github.com/ElliottBrand/BlazorForum\" target=\"_blank\">" +
                    "https://github.com/ElliottBrand/BlazorForum</a>.</p>",
                    IsIndex = true,
                    AllowDelete = false
                };
                await context.Pages.AddAsync(page);
                await context.SaveChangesAsync();
            }
        }
    }
}
