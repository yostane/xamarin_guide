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


        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var postDetailPage = new PostDetail(e.SelectedItem as Post);
            await Navigation.PushAsync(postDetailPage);
        }
    }
}
