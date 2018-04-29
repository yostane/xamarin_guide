using System;
using System.Collections.ObjectModel;
using RandomListXamarin.Model;
using System.Threading.Tasks;
using ItemsDetailXamarin.Helpers;
using Xamarin.Forms.Internals;
using Xamarin.Forms;
using System.Windows.Input;
using PostListDetailsXamarin.Views;
using System.Linq;
using System.Collections.Generic;

namespace RandomListXamarin.ViewModels
{
    public class ItemListViewModel : BaseViewModel
    {

        public ObservableCollection<Post> Posts { get; set; }
        public ICommand OpenPostCommand { get; private set; }

        public ICommand RefreshCommand { get; private set; }

        public ItemListViewModel()
        {
            this.Posts = new ObservableCollection<Post>();

            RefreshCommand = new Command(async () => await UpdatePostsAsync());
        }

        public async Task UpdatePostsAsync()
        {
            var newPosts = await JsonPlaceholderHelper.GetPostsAsync();
            this.Posts.Clear();
            newPosts.ForEach((post) =>
            {
                post.ImageUrl = "https://picsum.photos/70/?image=" + newPosts.IndexOf(post);
                this.Posts.Add(post);
            });
        }

    }
}
