using BlazorForum.Domain.Interfaces;
using BlazorForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorForum.Tests.Domain.Helpers.Forum
{
    public class AddPostHelpersTests
    {
        Mock<IHttpContextAccessor> mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        public AddPostHelpersTests()
        {
            mockHttpContextAccessor.Setup(p => p.HttpContext.Request.Scheme).Returns(It.IsAny<string>());
            mockHttpContextAccessor.Setup(p => p.HttpContext.Request.Host).Returns(It.IsAny<HostString>());
        }

        [Fact]
        async void AddSubscriptionAndSendEmailToSubscribersSuccessfulTest()
        {
            var mockManageTopicSubscriptions = new Mock<IManageTopicSubscriptions>();
            mockManageTopicSubscriptions.Setup(p => p.SubscribeUserToTopicAsync(It.IsAny<TopicSubscription>()))
                .Returns(Task.FromResult(true));

            var mockEmailNotificationsService = new Mock<IEmailNotificationsService>();
            mockEmailNotificationsService.Setup(p => p.SendTopicReplyEmailNotificationAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var response = await new BlazorForum.Domain.Helpers.Forum.AddPostHelpers(mockManageTopicSubscriptions.Object, mockHttpContextAccessor.Object, mockEmailNotificationsService.Object, NullLoggerFactory.Instance)
                .AddSubscriptionAndSendEmailToSubscribersAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.IsType<bool>(response);
            Assert.True(response);
        }

        [Fact]
        async void AddSubscriptionAndSendEmailToSubscribersFailedReturnsFalseTest()
        {
            var mockManageTopicSubscriptions = new Mock<IManageTopicSubscriptions>();
            mockManageTopicSubscriptions.Setup(p => p.SubscribeUserToTopicAsync(It.IsAny<TopicSubscription>()))
                .Callback(() => throw new Exception("Some Exception"));

            var mockEmailNotificationsService = new Mock<IEmailNotificationsService>();
            mockEmailNotificationsService.Setup(p => p.SendTopicReplyEmailNotificationAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var response = await new BlazorForum.Domain.Helpers.Forum.AddPostHelpers(mockManageTopicSubscriptions.Object, mockHttpContextAccessor.Object, mockEmailNotificationsService.Object, NullLoggerFactory.Instance)
                .AddSubscriptionAndSendEmailToSubscribersAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.IsType<bool>(response);
            Assert.False(response);
        }
    }
}
