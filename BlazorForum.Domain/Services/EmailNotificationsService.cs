using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorForum.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace BlazorForum.Domain.Services
{
    public class EmailNotificationsService : IEmailNotificationsService
    {
        private readonly IManageForumTopics _manageForumTopics;
        private readonly IManageUsers _manageUsers;
        private readonly IEmailSender _emailSender;
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;
        private readonly ILogger _logger;

        public EmailNotificationsService(IManageForumTopics manageForumTopics, IManageUsers manageUsers, IEmailSender emailSender,
            IManageTopicSubscriptions manageTopicSubscriptions, ILoggerFactory loggerFactory)
        {
            _manageForumTopics = manageForumTopics;
            _manageUsers = manageUsers;
            _emailSender = emailSender;
            _manageTopicSubscriptions = manageTopicSubscriptions;
            _logger = loggerFactory.CreateLogger("EmailNotificationsServiceCategory");
        }

        public async Task<bool> SendTopicReplyEmailNotificationAsync(int topicId, string currentUserId, string url)
        {
            try
            {
                var topic = await _manageForumTopics.GetForumTopicAsync(topicId);
                var currentUser = await _manageUsers.GetUserAsync(currentUserId);
                var topicSubscriptions = await _manageTopicSubscriptions.GetSubscriptionsForTopicAsync(topicId);

                foreach (var subscription in topicSubscriptions.Where(p => p.Id != currentUser.Id).ToList())
                {
                    var user = await _manageUsers.GetUserAsync(subscription.Id);
                    await _emailSender.SendEmailAsync(user.Email, "RE: " + topic.Title, currentUser.UserName + " has posted a reply to a topic that you're subscribed to at: <a href=\"" + url + "\">" + url + "</a>.");
                }

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was a problem emailing the topic reply notification(s)");

                return false;
            }
        }
    }
}
