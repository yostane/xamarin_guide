using System;
using GalaSoft.MvvmLight.Ioc;
using MvvmLight.XamarinForms;
using ItemsDetailXamarin.Views;
using PostListDetailsXamarin.Views;
using RandomListXamarin.ViewModels;
using CommonServiceLocator;
namespace PostListDetailsXamarin.ViewModels
{
    public class Locator
    {
        public const string ItemListPage = "ItemListPage";
        public const string PostDetailPage = "PostDetail";

        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //register the viewmdoels
            SimpleIoc.Default.Register<ItemListViewModel>();


            //register the navigation service
            var navigation = new NavigationService();
            navigation.Configure(ItemListPage, typeof(ItemListPage));
            navigation.Configure(PostDetailPage, typeof(PostDetail));
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
