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

        ApplicationUser GetUser(string userId);

        Task<ApplicationUser> GetUserByUserNameAsync(string username);

        Task<bool> IsInRoleAsync(string role, string userId);

        Task<int> GetUserPostAnswerCountAsync(string userId);

        Task<int> GetUserTopicQuestionCountAsync(string userId);

        Task<int> GetUserPostVoteTotal(string userId);
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

        public ApplicationUser GetUser(string userId) =>
            new Data.Repository.Users(_dbFactory).GetUser(userId);

        public async Task<ApplicationUser> GetUserByUserNameAsync(string username) =>
            await new Data.Repository.Users(_dbFactory).GetUserByUserNameAsync(username);

        public async Task<bool> IsInRoleAsync(string role, string userId) =>
            await new Data.Repository.Users(_dbFactory).IsInRoleAsync(role, userId);

        public async Task<int> GetUserPostAnswerCountAsync(string userId) =>
            await new Data.Repository.Users(_dbFactory).GetUserPostAnswerCountAsync(userId);

        public async Task<int> GetUserTopicQuestionCountAsync(string userId) =>
            await new Data.Repository.Users(_dbFactory).GetUserTopicQuestionCountAsync(userId);

        public async Task<int> GetUserPostVoteTotal(string userId) =>
            await new Data.Repository.Users(_dbFactory).GetUserPostVoteTotal(userId);
    }
}
