using BlazorForum.Data;
using BlazorForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageUsers
    {
        Task<ApplicationUser> GetUserAsync(string userId);

        Task<bool> IsInRoleAsync(string role, string userId);
    }

    public class ManageUsers : IManageUsers
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManageUsers(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ApplicationUser> GetUserAsync(string userId) =>
            await new Data.Repository.Users(_dbFactory).GetUserAsync(userId);

        public async Task<bool> IsInRoleAsync(string role, string userId) =>
            await new Data.Repository.Users(_dbFactory).IsInRoleAsync(role, userId);
    }
}
