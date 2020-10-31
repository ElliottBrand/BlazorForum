using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Interfaces
{
    public interface IEmailNotificationsService
    {
        Task<bool> SendTopicReplyEmailNotificationAsync(int topicId, string currentUserId, string url);
    }
}
