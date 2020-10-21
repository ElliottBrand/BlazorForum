using BlazorForum.Tests.Domain.Helpers.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Tests.FakeData
{
    public static class FakeForumData
    {
        public static List<Models.ForumTopic> BuildForumTopicsList(int listCount)
        {
            var list = new List<Models.ForumTopic>();

            for (int i = 0; i < listCount; i++)
            {
                list.Add(BuildFakeForumTopic(i+1));
            }

            return list;
        }

        public static Models.ForumTopic BuildFakeForumTopic(int forumTopicId)
        {
            return new Models.ForumTopic()
            {
                ForumTopicId = forumTopicId
            };
        }

        public static List<Models.TopicPostCount> BuildTopicPostCountList(int listCount, int childPostsCount)
        {
            var list = new List<Models.TopicPostCount>();

            for (int i = 0; i < listCount; i++)
            {
                list.Add(BuildFakeTopicPostCount(i + 1, childPostsCount));
            }

            return list;
        }

        public static Models.TopicPostCount BuildFakeTopicPostCount(int parentItemId, int childCount)
        {
            return new Models.TopicPostCount()
            {
                ParentItemId = parentItemId,
                ChildCount = childCount
            };
        }
    }
}
