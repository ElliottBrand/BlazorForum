using BlazorForum.Domain.Interfaces;
using BlazorForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Helpers.Forum
{
    public class AddPostHelpers
    {
        private readonly IManageTopicSubscriptions _manageTopicSubscriptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailNotificationsService _emailNotificationsService;
        private readonly ILogger _logger;

        public AddPostHelpers(IManageTopicSubscriptions manageTopicSubscriptions, IHttpContextAccessor httpContextAccessor, 
            IEmailNotificationsService emailNotificationsService, ILoggerFactory loggerFactory)
        {
            _manageTopicSubscriptions = manageTopicSubscriptions;
            _httpContextAccessor = httpContextAccessor;
            _emailNotificationsService = emailNotificationsService;
            _logger = loggerFactory.CreateLogger("AddSendSubscriptionsCategory");
        }

        public async Task<bool> AddSubscriptionAndSendEmailToSubscribersAsync(int topicId, string currentUserId, string topicSlug)
        {
            try
            {
                var newTopicSubscription = new TopicSubscription { ForumTopicId = topicId, Id = currentUserId };
                var subscriptionAdded = await _manageTopicSubscriptions.SubscribeUserToTopicAsync(newTopicSubscription);

                string url = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/topic/" + topicId + "/" + topicSlug;
                await _emailNotificationsService.SendTopicReplyEmailNotificationAsync(topicId, currentUserId, url);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);

                return false;
            }
        }
    }
}
