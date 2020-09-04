using BlazorForum.Domain.Interfaces;
using BlazorForum.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Helpers.Forum
{
    public class ForumUserHelpers
    {
        private readonly IManageUsers _userManager;

        public ForumUserHelpers(IManageUsers userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserToTopicAsync(List<ForumTopic> topics)
        {
            foreach (var topic in topics)
            {
                var user = await _userManager.FindByIdAsync(topic.UserId);
                topic.UserName = user.UserName;
            }
        }

        public async Task AddUserToPostAsync(List<ForumPost> posts)
        {
            foreach (var post in posts)
            {
                var user = await _userManager.FindByIdAsync(post.UserId);
                post.UserName = user.UserName;
            }
        }
    }
}
