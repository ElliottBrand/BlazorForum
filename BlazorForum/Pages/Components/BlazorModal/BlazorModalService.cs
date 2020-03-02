using System;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorForum.Pages.Components.BlazorModal
{
    public class BlazorModalService : IBlazorModal
    {
        public event Action<string, RenderFragment> OnShow;

        public event Action OnClose;

        // IMPORTANT - Don't reuse these in more than one location, as they would both fire and cause issues
        public event Action OnConfirmDeleteForum;
        public event Action OnConfirmDeleteForumCategory;
        public event Action OnConfirmDeleteForumCategoryItem;
        public event Action OnConfirmDeleteForumTopic;
        public event Action OnConfirmDeleteForumTopicItem;
        public event Action OnConfirmDeleteForumPostItem;

        public void Show<T>(string title, params BlazorModalParameter[] parameters) where T : ComponentBase
        {
            var content = new RenderFragment(x =>
            {
                var idx = 1;
                x.OpenComponent(idx++, typeof(T));
                if (parameters != null && parameters.Any())
                {
                    foreach (var param in parameters)
                    {
                        x.AddAttribute(idx++, param.Name, param.Value);
                    }
                }
                x.CloseComponent();
            });

            OnShow?.Invoke(title, content);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }

        public void ConfirmYes(string itemToDelete)
        {
            if (itemToDelete == "forum")
                OnConfirmDeleteForum?.Invoke();
            else if (itemToDelete == "category")
                OnConfirmDeleteForumCategory?.Invoke();
            else if (itemToDelete == "category-item")
                OnConfirmDeleteForumCategoryItem?.Invoke();
            else if (itemToDelete == "topic")
                OnConfirmDeleteForumTopic?.Invoke();
            else if (itemToDelete == "topic-item")
                OnConfirmDeleteForumTopicItem?.Invoke();
            else if (itemToDelete == "post-item")
                OnConfirmDeleteForumPostItem?.Invoke();
        }
    }
}
