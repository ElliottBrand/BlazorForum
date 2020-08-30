using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class Themes
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public Themes(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<string> GetSelectedThemeNameAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var theme = await context.Themes.Where(p => p.IsSelected == true && !String.IsNullOrEmpty(p.TextDomain.Trim())).FirstOrDefaultAsync();
            return theme?.TextDomain;
        }

        public async Task<bool> RemoveThemesAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var themes = context.Themes;
            if(themes != null)
            {
                foreach(var theme in themes.ToList())
                {
                    themes.Remove(theme);
                }
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> AddThemeAsync(string textDomain)
        {
            using var context = _dbFactory.CreateDbContext();
            var themes = context.Themes;
            var theme = new Theme
            {
                TextDomain = textDomain,
                IsSelected = true
            };
            themes.Add(theme);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
