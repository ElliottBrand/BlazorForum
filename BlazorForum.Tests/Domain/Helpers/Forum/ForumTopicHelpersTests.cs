using BlazorForum.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorForum.Tests.Domain.Helpers.Forum
{
    public class ForumTopicHelpersTests
    {
        Mock<IManageTopicSubscriptions> mockManageTopicSubscriptions = new Mock<IManageTopicSubscriptions>();

        [Fact]
        async void CurrentUserIsSubscribedToTopicTest()
        {
            string userId = "1";
            int postId = 10;

            mockManageTopicSubscriptions.Setup(p => p.GetSubscriptionsForTopicAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeData.FakeTopicSubscriptionData.GetTopicSubscriptions(userId, postId)));

            var result = await new BlazorForum.Domain.Helpers.Forum.ForumTopicHelpers(mockManageTopicSubscriptions.Object).CurrentUserIsSubscribedToTopic(userId, postId);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        async void CurrentUserIsSubscribedToTopicNullUserIdTest()
        {
            string userId = null;
            int postId = 10;

            mockManageTopicSubscriptions.Setup(p => p.GetSubscriptionsForTopicAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeData.FakeTopicSubscriptionData.GetTopicSubscriptions(userId, postId)));

            var result = await new BlazorForum.Domain.Helpers.Forum.ForumTopicHelpers(mockManageTopicSubscriptions.Object).CurrentUserIsSubscribedToTopic(userId, postId);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
