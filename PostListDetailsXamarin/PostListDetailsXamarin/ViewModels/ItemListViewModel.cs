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
using RandomListXamarin.ViewModels;

namespace PostListDetailsXamarin.ViewModels
{
	public class ItemListViewModel : BaseViewModel
	{

		public ObservableCollection<Post> Posts { get; set; }

		//The command taks a Post as a parameter, because it is the type of the selected item
		public ICommand ItemSelectedCommand => new Command<Post>(selectedItem =>
		{
			App.Locator.PostDetailViewModel.Post = selectedItem;
			App.Locator.NavigationService.NavigateTo(Locator.PostDetailPage);
		});

		public ICommand RefreshCommand => new Command(async () => await UpdatePostsAsync());

		public ItemListViewModel()
		{
			this.Posts = new ObservableCollection<Post>();
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
