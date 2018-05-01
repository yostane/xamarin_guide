using Xamarin.Forms;
using RandomListXamarin.ViewModels;
using PostListDetailsXamarin.Views;
using RandomListXamarin.Model;

namespace ItemsDetailXamarin.Views
{
	public partial class ItemListPage : ContentPage
	{
		ItemListViewModel itemListViewModel;

		public ItemListPage()
		{
			InitializeComponent();
			itemListViewModel = BindingContext as ItemListViewModel;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await itemListViewModel.UpdatePostsAsync();
		}
	}
}
