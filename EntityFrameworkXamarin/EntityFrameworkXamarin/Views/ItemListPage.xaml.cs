using Xamarin.Forms;
using PostListDetailsXamarin.ViewModels;

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
