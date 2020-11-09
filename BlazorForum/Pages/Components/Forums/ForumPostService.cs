using BlazorForum.Domain.Helpers.Forum;
using BlazorForum.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorForum.Pages.Components.Forums
{
    public class ForumPostService
    {
        private readonly IManageForumPosts _manageForumPosts;
        private readonly IManageUsers _manageUsers;

        public ForumPostService(IManageForumPosts manageForumPosts, IManageUsers manageUsers)
        {
            _manageForumPosts = manageForumPosts;
            _manageUsers = manageUsers;
        }

        public event EventHandler OnPosted;
        public event EventHandler OnAnswerStatusChanged;

        // This needs to have posts cleared/removed somehow when no user is browsing a topic that has posts stored in the dictionary
        public ConcurrentDictionary<int, List<Models.ForumPost>> PostsDictionary { get; set; } = new ConcurrentDictionary<int, List<Models.ForumPost>>();

        public virtual void NotifyStateChanged() => OnPosted?.Invoke(this, EventArgs.Empty);
        public virtual void NotifyAnswerStatusChanged() => OnAnswerStatusChanged?.Invoke(this, EventArgs.Empty);

        public async Task LoadPostsAsync(int id)
        {
            var posts = await _manageForumPosts.GetApprovedForumPostsAsync(id);

            await new ForumUserHelpers(_manageUsers).AddUserToPostAsync(posts);

            List<Models.ForumPost> currentPosts;
            var topicHasPosts = PostsDictionary.TryGetValue(id, out currentPosts);
            if (topicHasPosts)
                PostsDictionary.TryUpdate(id, posts, currentPosts);
            else
                PostsDictionary.TryAdd(id, posts);
        }

        public async Task RefreshPostsAsync(int id)
        {
            var posts = await _manageForumPosts.GetApprovedForumPostsAsync(id);

            await new ForumUserHelpers(_manageUsers).AddUserToPostAsync(posts);

            List<Models.ForumPost> currentPosts;
            var topicHasPosts = PostsDictionary.TryGetValue(id, out currentPosts);
            if (topicHasPosts)
                PostsDictionary.TryUpdate(id, posts, currentPosts);
            else
                PostsDictionary.TryAdd(id, posts);

            NotifyStateChanged();
        }

        public void RefreshAnswerStatus()
        {
            NotifyAnswerStatusChanged();
        }
    }
}
