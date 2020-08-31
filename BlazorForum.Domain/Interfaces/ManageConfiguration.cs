using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Data;
using BlazorForum.Models;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageConfiguration
    {
        Configuration GetConfig();
        Task<Configuration> GetConfigAsync();
        Task<bool> UpdateConfigAsync(Configuration editedConfig);
    }

    public class ManageConfiguration : IManageConfiguration
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManageConfiguration(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Configuration GetConfig() => new Data.Repository.SiteConfiguration(_dbFactory).GetConfig();

        public async Task<Configuration> GetConfigAsync() => 
            await new Data.Repository.SiteConfiguration(_dbFactory).GetConfigAsync();

        public async Task<bool> UpdateConfigAsync(Configuration editedConfig) =>
            await new Data.Repository.SiteConfiguration(_dbFactory).UpdateConfigAsync(editedConfig);
    }
}
