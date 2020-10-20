using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorForum.Data.Repository
{
    public class UpDownVotes
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public UpDownVotes(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> GetPostUpDownVoteCountAsync(int postId, string uniqueIdentifier)
        {
            using var context = _dbFactory.CreateDbContext();
            var votes = await context.UpDownVotes.Where(p => p.PostId == postId).Where(p => p.UniqueIdentifier == uniqueIdentifier).ToListAsync();
            int count = 0;
            foreach (var vote in votes)
            {
                count += vote.VoteIncrement;
            }
            return count;
        }

        public async Task<bool> VoterHasVoted(string voterId, int postId, string uniqueIdentifier)
        {
            using var context = _dbFactory.CreateDbContext();
            var votes = await context.UpDownVotes.Where(p => p.PostId == postId && p.VoterId == voterId).Where(p => p.UniqueIdentifier == uniqueIdentifier).FirstOrDefaultAsync();
            if (votes != null)
                return true;
            return false;
        }

        public async Task<bool> AddPostUpDownVoteAsync(UpDownVote newUpDownVote)
        {
            using var context = _dbFactory.CreateDbContext();
            var votes = context.UpDownVotes;
            var vote = new UpDownVote
            {
                PostId = newUpDownVote.PostId,
                UniqueIdentifier = newUpDownVote.UniqueIdentifier,
                DateVoted = newUpDownVote.DateVoted,
                PosterId = newUpDownVote.PosterId,
                VoteIncrement = newUpDownVote.VoteIncrement,
                VoterId = newUpDownVote.VoterId,
                UpDownVoteId = newUpDownVote.UpDownVoteId
            };
            await votes.AddAsync(vote);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUpDownVotesByUserAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var votes = context.UpDownVotes;
            foreach (var vote in await votes.Where(p => p.VoterId == userId).ToListAsync())
            {
                votes.Remove(vote);
            }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUpDownVotesForUserAsync(string userId)
        {
            using var context = _dbFactory.CreateDbContext();
            var votes = context.UpDownVotes;
            foreach (var vote in await votes.Where(p => p.PosterId == userId).ToListAsync())
            {
                votes.Remove(vote);
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
