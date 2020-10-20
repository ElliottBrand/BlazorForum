using BlazorForum.Data;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IManageUpDownVotes
    {
        Task<int> GetPostUpDownVoteCountAsync(int postId);
        Task<bool> AddPostUpDownVoteAsync(UpDownVote newUpDownVote);
        Task<bool> VoterHasVoted(string voterId, int postId);
        Task<bool> DeleteUpDownVotesByUserAsync(string userId);
        Task<bool> DeleteUpDownVotesForUserAsync(string userId);
    }

    public class ManageUpDownVotes : IManageUpDownVotes
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ManageUpDownVotes(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> GetPostUpDownVoteCountAsync(int postId) =>
            await new Data.Repository.UpDownVotes(_dbFactory).GetPostUpDownVoteCountAsync(postId);

        public async Task<bool> AddPostUpDownVoteAsync(UpDownVote newUpDownVote) =>
            await new Data.Repository.UpDownVotes(_dbFactory).AddPostUpDownVoteAsync(newUpDownVote);

        public async Task<bool> VoterHasVoted(string voterId, int postId) =>
            await new Data.Repository.UpDownVotes(_dbFactory).VoterHasVoted(voterId, postId);

        public async Task<bool> DeleteUpDownVotesByUserAsync(string userId) =>
            await new Data.Repository.UpDownVotes(_dbFactory).DeleteUpDownVotesByUserAsync(userId);

        public async Task<bool> DeleteUpDownVotesForUserAsync(string userId) =>
            await new Data.Repository.UpDownVotes(_dbFactory).DeleteUpDownVotesForUserAsync(userId);
    }
}
