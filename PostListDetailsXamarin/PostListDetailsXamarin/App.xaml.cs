using Xamarin.Forms;
using ItemsDetailXamarin.Views;
using PostListDetailsXamarin.ViewModels;

namespace PostListDetailsXamarin
{
    public partial class App : Application
    {
        //static app wide locator instance
        private static Locator _locator;
        public static Locator Locator
        {
            get
            {
                return _locator ?? (_locator = new Locator());
            }
        }

        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new ItemListPage());
            Locator.NavigationService.Initialize(navigationPage);
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
