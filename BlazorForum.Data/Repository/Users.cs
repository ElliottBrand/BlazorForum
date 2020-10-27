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

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Users.SingleAsync(p => p.Id == userId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string username)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.Users.SingleOrDefaultAsync(p => p.UserName == username);
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

        /// <summary>
        /// Gets the count for the number of user posts that are marked as answers.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetUserPostAnswerCountAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.ForumPosts.Where(p => p.UserId == userId).Where(p => p.IsAnswer == true).CountAsync();
        }

        /// <summary>
        /// Gets the count for the number of topics created by a user in a forum that is marked as a support forum.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetUserTopicQuestionCountAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            int topicsCount = 0;
            var supportForumCategories = new List<ForumCategories>();

            var supportForums = await context.Forums.Where(p => p.IsSupportForum == true).ToListAsync();
            foreach (var forum in supportForums)
            {
                var categories = await context.ForumCategories.Where(p => p.ForumId == forum.ForumId).ToListAsync();
                foreach(var cat in categories)
                {
                    var count = await context.ForumTopics.Where(p => p.ForumCategoryId == cat.ForumCategoryId).Where(p => p.UserId == userId).CountAsync();
                    topicsCount += count;
                }
            }
            return topicsCount;
        }

        /// <summary>
        /// Gets the total vote count the user's topics and posts have received.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetUserPostVoteTotal(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var total = 0;
            var votes = await context.UpDownVotes.Where(p => p.PosterId == userId).ToListAsync();
            foreach (var vote in votes)
            {
                total += vote.VoteIncrement;
            }
            return total;
        }
    }
}
