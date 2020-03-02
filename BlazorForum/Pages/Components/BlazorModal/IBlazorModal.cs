using Microsoft.AspNetCore.Components;
using System;

namespace BlazorForum.Pages.Components.BlazorModal
{
    public interface IBlazorModal
    {
        event Action<string, RenderFragment> OnShow;
        event Action OnClose;
        event Action OnConfirmDeleteForum;
        event Action OnConfirmDeleteForumCategory;
        event Action OnConfirmDeleteForumCategoryItem;
        event Action OnConfirmDeleteForumTopic;
        event Action OnConfirmDeleteForumTopicItem;
        event Action OnConfirmDeleteForumPostItem;
        void Show<T>(string title, params BlazorModalParameter[] parameters) where T : ComponentBase;
        void Close();
        void ConfirmYes(string itemToDelete);
    }
}
