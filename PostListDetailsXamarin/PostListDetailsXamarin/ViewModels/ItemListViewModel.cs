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
using PostListDetailsXamarin.ViewModels;
using PostListDetailsXamarin;

namespace RandomListXamarin.ViewModels
{
	public class ItemListViewModel : BaseViewModel
	{

		public ObservableCollection<Post> Posts { get; set; }

		//The command taks a Post as a parameter, because it is the type of the selected item
		public ICommand ItemSelectedCommand => new Command<Post>((selectedItem) =>
		{
			System.Diagnostics.Debug.WriteLine("Command invoked for item: " + selectedItem.Title);
		});

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
