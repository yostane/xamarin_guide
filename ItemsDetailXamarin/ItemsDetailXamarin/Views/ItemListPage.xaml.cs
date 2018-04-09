using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RandomListXamarin.ViewModels;

namespace ItemsDetailXamarin.Views
{
    public partial class ItemListPage : ContentPage
    {
        ItemListViewModel itemListViewModel;

        public ItemListPage()
        {
            InitializeComponent();

            itemListViewModel = new ItemListViewModel();
            this.BindingContext = itemListViewModel;
        }
    }
}
