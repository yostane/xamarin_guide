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
using System.Net.Http;

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
			try
			{
				//Update from backend if possible and update database
				var newPosts = await JsonPlaceholderHelper.GetPostsAsync();
				await App.Locator.PostDatabaseHelper.AddOrUpdatePostsAsync(newPosts);
			}
			catch (HttpRequestException e)
			{
				System.Diagnostics.Debug.WriteLine($"HTTP request exception {e.Message}");
			}
			//Always dispay what's in the database
			var databasePosts = await App.Locator.PostDatabaseHelper.getPostsAsync();
			//This loop allows to replace only the items that changed in the observable collection
			for (int i = 0; i < databasePosts.Count; i += 1)
			{
				if (Posts.Count <= i)
				{
					Posts.Add(databasePosts[i]);
				}
				else if (databasePosts[i].Id != Posts[i].Id)
				{
					Posts[i] = databasePosts[i];
				}
			}
			//delete remaining items in the Posts collection
			for (int i = databasePosts.Count; i < Posts.Count; i += 1)
			{
				Posts.RemoveAt(i);
			}
		}

	}
}
