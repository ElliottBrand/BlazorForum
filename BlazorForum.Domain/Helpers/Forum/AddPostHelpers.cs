using BlazorForum.Domain.Interfaces;
using BlazorForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Helpers.Forum
{
    public class AddPostHelpers
    {
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;
        private readonly IManageForumTopics _manageForumTopics;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AddPostHelpers(IManageTopicSubscriptions manageTopicSubscriptions, IManageForumTopics manageForumTopics, IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _manageTopicSubscriptions = manageTopicSubscriptions;
            _manageForumTopics = manageForumTopics;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task AddSubscriptionAndSendEmailToSubscribers(int topicId, string currentUserId, string topicSlug)
        {
            try
            {
                var newTopicSubscription = new TopicSubscription { ForumTopicId = topicId, Id = currentUserId };
                var subscriptionAdded = await _manageTopicSubscriptions.AddSubscriptionToTopicAsync(newTopicSubscription);

                string url = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/topic/" + topicId + "/" + topicSlug;
                await new Utilities.ForumNotifications.EmailNotifications(_manageForumTopics, _userManager, _emailSender, _manageTopicSubscriptions)
                    .SendTopicReplyEmailNotification(topicId, currentUserId, url);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
