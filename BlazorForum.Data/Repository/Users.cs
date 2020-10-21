using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;
using Microsoft.AspNetCore.Identity;

namespace BlazorForum.Data.Repository
{
    public class Users
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public Users(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ApplicationUser> GetUserAsync(string UserId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Users.SingleAsync(p => p.Id == UserId);
        }

        public async Task<bool> IsInRoleAsync(string roleName, string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var roles = await context.UserRoles.Where(p => p.UserId == userId).ToListAsync();
            foreach (var role in roles)
            {
                var roleId = role.RoleId;
                var thisRole = await context.Roles.Where(p => p.Id == role.RoleId).FirstOrDefaultAsync();
                if (thisRole.Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
