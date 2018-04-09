using System;
using System.Collections.ObjectModel;
using RandomListXamarin.Model;

namespace RandomListXamarin.ViewModels
{
    public class ItemListViewModel : BaseViewModel
    {
        public ObservableCollection<Post> posts { get; }

        public ItemListViewModel()
        {
            posts = new ObservableCollection<Post>();
        }

    }
}
