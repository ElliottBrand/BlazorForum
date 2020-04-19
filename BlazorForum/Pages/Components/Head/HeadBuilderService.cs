using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorForum.Pages.Components.Head
{
    public class HeadBuilderService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _title;
        private string _description;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
    }
}
