using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorForum.Pages.Components.UpDownVote
{
    public class UpDownVoteState
    {
        public event EventHandler OnVoteChanged;

        public virtual void NotifyVoteChanged() => OnVoteChanged?.Invoke(this, EventArgs.Empty);

        public void RefreshVoteCount()
        {
            NotifyVoteChanged();
        }
    }
}
