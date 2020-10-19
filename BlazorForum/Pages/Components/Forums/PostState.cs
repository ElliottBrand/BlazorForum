using BlazorForum.Domain.Helpers.Forum;
using BlazorForum.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorForum.Pages.Components.Forums
{
    public class PostState
    {
        private readonly IManageForumPosts _manageForumPosts;
        private readonly IManageUsers _manageUsers;

        public PostState(IManageForumPosts manageForumPosts, IManageUsers manageUsers)
        {
            _manageForumPosts = manageForumPosts;
            _manageUsers = manageUsers;
        }

        public Action OnPosted;
        public List<Models.ForumPost> Posts { get; private set; }
        public int Id { get; set; }
        public int MaxCount = 10;
        public string LoadMoreVisibility;

        public async Task LoadPostsAsync()
        {
            Posts = await _manageForumPosts.GetApprovedForumPostsAsync(Id);

            if (MaxCount >= Posts.Count)
                LoadMoreVisibility = "d-none";

            Posts = Posts.Take(MaxCount).ToList();
            await new ForumUserHelpers(_manageUsers).AddUserToPostAsync(Posts);
        }

        public async Task LoadMorePostsAsync()
        {
            MaxCount += 10;
            Posts = await _manageForumPosts.GetApprovedForumPostsAsync(Id);

            if (MaxCount >= Posts.Count)
                LoadMoreVisibility = "d-none";
            else
                LoadMoreVisibility = "d-inline-block";

            Posts = Posts.Take(MaxCount).ToList();
            await new ForumUserHelpers(_manageUsers).AddUserToPostAsync(Posts);
        }

        public async Task RefreshPostsAsync()
        {
            Posts = await _manageForumPosts.GetApprovedForumPostsAsync(Id);

            if (MaxCount >= Posts.Count)
                LoadMoreVisibility = "d-none";
            else
                LoadMoreVisibility = "d-inline-block";

            Posts = Posts.Take(MaxCount).ToList();
            await new ForumUserHelpers(_manageUsers).AddUserToPostAsync(Posts);

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnPosted?.Invoke();
    }
}
