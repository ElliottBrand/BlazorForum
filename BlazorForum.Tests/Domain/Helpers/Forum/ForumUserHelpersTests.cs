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
    public class ForumUserHelpersTests
    {
        Mock<IManageUsers> mockManageUsers = new Mock<IManageUsers>();

        public ForumUserHelpersTests()
        {
            mockManageUsers.Setup(p => p.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(FakeData.FakeUserData.GetFakeUser("1")));
        }

        [Fact]
        async void AddUserToTopicAsyncTest()
        {
            var topics = new List<Models.ForumTopic>()
            {
                new Models.ForumTopic() { UserName = null }
            };

            await new BlazorForum.Domain.Helpers.Forum.ForumUserHelpers(mockManageUsers.Object)
                .AddUserToTopicAsync(topics);

            Assert.Equal("tester", topics.First().UserName);
        }

        [Fact]
        async void AddUserToPostAsyncTest()
        {
            var posts = new List<Models.ForumPost>()
            {
                new Models.ForumPost() { UserName = null }
            };

            await new BlazorForum.Domain.Helpers.Forum.ForumUserHelpers(mockManageUsers.Object)
                .AddUserToPostAsync(posts);

            Assert.Equal("tester", posts.First().UserName);
        }
    }
}
