using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BlazorForum.Domain.Helpers.Forum;
using BlazorForum.Domain.Interfaces;

namespace BlazorForum.Tests.Domain.Helpers.Forum
{
    // NOTE: Models namespace was left in to help prevent confusion between the classes and the components, since some are named the same
    public class ForumTopicCountHelpersTests
    {
        Mock<IManageForumPosts> mockForumPostsManager = new Mock<IManageForumPosts>();

        [Fact]
        async void GetTopicsPostCountListAsync_ReturnsTopicPostCountListTest()
        {
            var fakeForumTopics = FakeData.FakeForumData.BuildForumTopicsList(2);
            
            mockForumPostsManager.Setup(p => p.GetApprovedForumPostsAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Models.ForumPost>()));

            var result = await new ForumTopicCountHelpers(mockForumPostsManager.Object).GetTopicsPostCountListAsync(fakeForumTopics);

            Assert.IsType<List<Models.TopicPostCount>>(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        void GetTopicCount_ReturnsZeroTopicCountWhenEmptyTest()
        {
            var fakeForumTopics = FakeData.FakeForumData.BuildForumTopicsList(2);

            var result = ForumTopicCountHelpers.GetTopicCount(new List<Models.TopicPostCount>(), 1);

            Assert.Equal(0, result);
        }

        [Fact]
        void GetTopicCount_ReturnsZeroTopicCountWhenNullTest()
        {
            var result = ForumTopicCountHelpers.GetTopicCount(null, 1);

            Assert.Equal(0, result);
        }

        [Fact]
        void GetTopicCount_ReturnsTopicCountTest()
        {
            var fakeForumTopics = FakeData.FakeForumData.BuildForumTopicsList(2);

            var result = ForumTopicCountHelpers.GetTopicCount(FakeData.FakeForumData.BuildTopicPostCountList(3, 2), 1);

            Assert.IsType<int>(result);
            Assert.Equal(2, result);
        }
    }
}
