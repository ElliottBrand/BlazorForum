using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class SiteConfiguration
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public SiteConfiguration(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Configuration GetConfig()
        {
            using var context = _dbFactory.CreateDbContext();
            return context.Configuration.FirstOrDefault();
        }

        public async Task<Configuration> GetConfigAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var config = await context.Configuration.FirstOrDefaultAsync();
            if (config != null)
                return config;
            else
                return await CreateConfigAsync();
        }

        private async Task<Configuration> CreateConfigAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            var newConfig = new Configuration
            {
                AnalyticsCode = null
            };
            await context.Configuration.AddAsync(newConfig);
            await context.SaveChangesAsync();
            return await context.Configuration.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateConfigAsync(Configuration editedConfig)
        {
            using var context = _dbFactory.CreateDbContext();
            var config = await context.Configuration.FirstOrDefaultAsync();
            if(config != null)
            {
                config.AnalyticsCode = editedConfig.AnalyticsCode;
                config.EmailAddress = editedConfig.EmailAddress;
                config.EmailSenderName = editedConfig.EmailSenderName;
                config.SendGridKey = editedConfig.SendGridKey;
                config.RegistrationApprovalMessage = editedConfig.RegistrationApprovalMessage;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
