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
        private ApplicationDbContext _context;

        public SiteConfiguration(ApplicationDbContext context)
        {
            _context = context;
        }

        public Configuration GetConfig()
        {
            return _context.Configuration.FirstOrDefault();
        }

        public async Task<Configuration> GetConfigAsync()
        {
            var config = await _context.Configuration.FirstOrDefaultAsync();
            if (config != null)
                return config;
            else
                return await CreateConfigAsync();
        }

        private async Task<Configuration> CreateConfigAsync()
        {
            var newConfig = new Configuration
            {
                AnalyticsCode = null
            };
            await _context.Configuration.AddAsync(newConfig);
            await _context.SaveChangesAsync();
            return await _context.Configuration.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateConfigAsync(Configuration editedConfig)
        {
            var config = await _context.Configuration.FirstOrDefaultAsync();
            if(config != null)
            {
                config.AnalyticsCode = editedConfig.AnalyticsCode;
                config.EmailAddress = editedConfig.EmailAddress;
                config.EmailSenderName = editedConfig.EmailSenderName;
                config.SendGridKey = editedConfig.SendGridKey;
                config.RegistrationApprovalMessage = editedConfig.RegistrationApprovalMessage;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
