using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RandomListXamarin.ViewModels;
using RandomListXamarin.Model;

namespace ItemsDetailXamarin.Views
{
    public partial class ItemListPage : ContentPage
    {
        ItemListViewModel itemListViewModel;

        public ItemListPage()
        {
            InitializeComponent();
            itemListViewModel = new ItemListViewModel();
            BindingContext = itemListViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await itemListViewModel.UpdatePostsAsync();
        }
    }
}
