using BlazorForum.Data;
using BlazorForum.Models;
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
        private readonly ApplicationDbContext _context;

        public ManageUpDownVotes(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetPostUpDownVoteCountAsync(int postId) =>
            await new Data.Repository.UpDownVotes(_context).GetPostUpDownVoteCountAsync(postId);

        public async Task<bool> AddPostUpDownVoteAsync(UpDownVote newUpDownVote) =>
            await new Data.Repository.UpDownVotes(_context).AddPostUpDownVoteAsync(newUpDownVote);

        public async Task<bool> VoterHasVoted(string voterId, int postId) =>
            await new Data.Repository.UpDownVotes(_context).VoterHasVoted(voterId, postId);

        public async Task<bool> DeleteUpDownVotesByUserAsync(string userId) =>
            await new Data.Repository.UpDownVotes(_context).DeleteUpDownVotesByUserAsync(userId);

        public async Task<bool> DeleteUpDownVotesForUserAsync(string userId) =>
            await new Data.Repository.UpDownVotes(_context).DeleteUpDownVotesForUserAsync(userId);
    }
}
