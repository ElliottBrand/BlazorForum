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
        private readonly ApplicationDbContext _context;

        public UpDownVotes(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetPostUpDownVoteCountAsync(int postId)
        {
            var votes = await _context.UpDownVotes.Where(p => p.PostId == postId).ToListAsync();
            int count = 0;
            foreach (var vote in votes)
            {
                count += vote.VoteIncrement;
            }
            return count;
        }

        public async Task<bool> VoterHasVoted(string voterId, int postId)
        {
            var votes = await _context.UpDownVotes.Where(p => p.PostId == postId && p.VoterId == voterId).FirstOrDefaultAsync();
            if (votes != null)
                return true;
            return false;
        }

        public async Task<bool> AddPostUpDownVoteAsync(UpDownVote newUpDownVote)
        {
            var votes = _context.UpDownVotes;
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
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUpDownVotesByUserAsync(string userId)
        {
            var votes = _context.UpDownVotes;
            foreach (var vote in await votes.Where(p => p.VoterId == userId).ToListAsync())
            {
                votes.Remove(vote);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUpDownVotesForUserAsync(string userId)
        {
            var votes = _context.UpDownVotes;
            foreach (var vote in await votes.Where(p => p.PosterId == userId).ToListAsync())
            {
                votes.Remove(vote);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
