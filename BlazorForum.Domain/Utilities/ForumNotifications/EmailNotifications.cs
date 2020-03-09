using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Domain.Interfaces;
using BlazorForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BlazorForum.Domain.Utilities.ForumNotifications
{
    public class EmailNotifications
    {
        private readonly IManageForumTopics _manageForumTopics;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;

        public EmailNotifications(IManageForumTopics manageForumTopics, UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            IManageTopicSubscriptions manageTopicSubscriptions)
        {
            _manageForumTopics = manageForumTopics;
            _userManager = userManager;
            _emailSender = emailSender;
            _manageTopicSubscriptions = manageTopicSubscriptions;
        }

        public async Task SendTopicReplyEmailNotification(int topicId, string currentUserId, string url)
        {
            var topic = await _manageForumTopics.GetForumTopicAsync(topicId);
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var topicSubscriptions = await _manageTopicSubscriptions.GetSubscriptionsForTopicAsync(topicId);

            foreach (var subscription in topicSubscriptions.Where(p => p.Id != currentUser.Id).ToList())
            {
                var user = await _userManager.FindByIdAsync(subscription.Id);
                await _emailSender.SendEmailAsync(user.Email, "RE: " + topic.Title, currentUser.UserName + " has posted a reply to a topic that you're subscribed to at: <a href=\"" + url + "\">" + url + "</a>.");
            }  
        }
    }
}
