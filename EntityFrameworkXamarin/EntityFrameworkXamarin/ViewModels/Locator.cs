using System;
using GalaSoft.MvvmLight.Ioc;
using MvvmLight.XamarinForms;
using ItemsDetailXamarin.Views;
using PostListDetailsXamarin.Views;

namespace PostListDetailsXamarin.ViewModels
{
	/// <summary>
	/// Allows to get a reference to the view models and the navigation service
	/// </summary>
	public class Locator
	{
		/// <summary>
		/// The key that allows to reference the ItemListPage using the navigation service
		/// </summary>
		public const string ItemListPage = "ItemListPage";
		/// <summary>
		/// The key that allows to reference the PostDetailPage using the navigation service
		/// </summary>
		public const string PostDetailPage = "PostDetail";

		static Locator()
		{
			//register the viewmdoels
			SimpleIoc.Default.Register<ItemListViewModel>();
			SimpleIoc.Default.Register<PostDetailViewModel>();

			//Create the navigation service
			var navigation = new NavigationService();
			//Configure the pages managed by the navigation service. Each page is referenced by a key.
			navigation.Configure(ItemListPage, typeof(ItemListPage));
			navigation.Configure(PostDetailPage, typeof(PostDetail));
			SimpleIoc.Default.Register(() => navigation);
		}

		#region getters

		public NavigationService NavigationService => SimpleIoc.Default.GetInstance<NavigationService>();
		public ItemListViewModel ItemListViewModel => SimpleIoc.Default.GetInstance<ItemListViewModel>();
		public PostDetailViewModel PostDetailViewModel => SimpleIoc.Default.GetInstance<PostDetailViewModel>();

		#endregion
	}
}
