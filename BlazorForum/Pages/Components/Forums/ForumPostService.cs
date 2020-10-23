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
        public List<Models.ForumPost> Posts { get; private set; }
        public int Id { get; set; }
        public int MaxCount = 10;
        public string LoadMoreVisibility;

        public virtual void NotifyStateChanged() => OnPosted?.Invoke(this, EventArgs.Empty);
        public virtual void NotifyAnswerStatusChanged() => OnAnswerStatusChanged?.Invoke(this, EventArgs.Empty);

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

        public void RefreshAnswerStatus()
        {
            NotifyAnswerStatusChanged();
        }
    }
}
