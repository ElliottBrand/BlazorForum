using BlazorForum.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageThemes
    {
        Task<string> GetSelectedThemeNameAsync();
        Task<bool> RemoveThemesAsync();
        Task<bool> AddThemeAsync(string textDomain);
    }

    public class ManageThemes : IManageThemes
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public string ThemeName { get; set; }

        public ManageThemes(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<string> GetSelectedThemeNameAsync() => 
            await new Data.Repository.Themes(_dbFactory).GetSelectedThemeNameAsync();

        public async Task<bool> RemoveThemesAsync() =>
            await new Data.Repository.Themes(_dbFactory).RemoveThemesAsync();

        public async Task<bool> AddThemeAsync(string textDomain) =>
            await new Data.Repository.Themes(_dbFactory).AddThemeAsync(textDomain);
    }
}
