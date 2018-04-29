using System;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using MvvmLight.XamarinForms;
using ItemsDetailXamarin.Views;
using PostListDetailsXamarin.Views;
using RandomListXamarin.ViewModels;
namespace PostListDetailsXamarin.ViewModels
{
    public class Locator
    {
        public const string ITEM_LIST_PAGE = "ItemListPage";
        public const string ITEM_DETAIL_PAGE = "PostDetail";

        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //register the viewmdoels
            SimpleIoc.Default.Register<ItemListViewModel>();


            //register the navigation service
            var navigation = new NavigationService();
            navigation.Configure(ITEM_LIST_PAGE, typeof(ItemListPage));
            navigation.Configure(ITEM_DETAIL_PAGE, typeof(PostDetail));
            SimpleIoc.Default.Register(() => navigation);
        }

        public NavigationService NavigationService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NavigationService>();
            }
        }

        public ItemListViewModel ItemListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ItemListViewModel>();
            }
        }
    }
}
