using BlazorForum.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorForum.Tests.Domain.Services
{
    public class EmailNotificationsServiceTests
    {

        Mock<ILoggerFactory> mockLogger = new Mock<ILoggerFactory>();
        Mock<IManageForumTopics> mockManageForumTopics = new Mock<IManageForumTopics>();
        Mock<IManageUsers> mockManageUsers = new Mock<IManageUsers>();
        Mock<IManageTopicSubscriptions> mockManageTopicSubscriptions = new Mock<IManageTopicSubscriptions>();
        Mock<IEmailSender> mockEmailSender = new Mock<IEmailSender>();

        [Fact]
        async void SendTopicReplyEmailNotificationsAsyncSuccessfulTest()
        {
            
            mockManageForumTopics.Setup(p => p.GetForumTopicAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeData.FakeForumData.BuildFakeForumTopic(55)));

            mockManageUsers.Setup(p => p.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(FakeData.FakeUserData.GetFakeUser("1")));

            mockManageTopicSubscriptions.Setup(p => p.GetSubscriptionsForTopicAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeData.FakeTopicSubscriptionData.GetTopicSubscriptions("2", 55)));

            mockEmailSender.Setup(p => p.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(""));

            var response = await new BlazorForum.Domain.Services.EmailNotificationsService(mockManageForumTopics.Object, mockManageUsers.Object, mockEmailSender.Object, mockManageTopicSubscriptions.Object, mockLogger.Object)
                .SendTopicReplyEmailNotificationAsync(55, "1", "some-url");

            Assert.IsType<bool>(response);
            Assert.True(response);
        }

        [Fact]
        async void SendTopicReplyEmailNotificationsAsyncExceptionTest()
        {
            var mockManageForumTopics = new Mock<IManageForumTopics>();
            mockManageForumTopics.Setup(p => p.GetForumTopicAsync(It.IsAny<int>()))
                .Callback(() => throw new Exception("Some Exception"));

            var response = await new BlazorForum.Domain.Services.EmailNotificationsService(mockManageForumTopics.Object, mockManageUsers.Object , mockEmailSender.Object, mockManageTopicSubscriptions.Object, mockLogger.Object)
                .SendTopicReplyEmailNotificationAsync(55, "1", "some-url");

            Assert.IsType<bool>(response);
            Assert.False(response);
        }
    }
}
