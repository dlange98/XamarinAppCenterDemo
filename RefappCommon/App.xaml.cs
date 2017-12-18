using Refapp.DAO;
using Refapp.Configuration;
using Refapp.Services;
using Xamarin.Forms;
using Refapp.Managers;
using Refapp.Models;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Refapp
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            var fileManager = DependencyService.Get<IFileManager>();
            var TokenDAO = new AccessTokenDAO(fileManager);

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();


            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());

            // clear out current user. This will force the need to login each time the app is restarted
            TokenDAO.DeleteToken();
            DependencyService.Get<IAuthenticator>().ClearToken(Settings.TenantId);

        }

        protected override void OnStart()
        {
            base.OnStart();

            // app center
            AppCenter.Start("android=4c4f4341-26f6-4b9d-b67f-77b65b337f0a;" + 
                            "ios=29397e44-73e2-4fd1-a23f-0a32e118063f",
                   typeof(Analytics), typeof(Crashes));

            loginIfNeeded();
        }

        async void loginIfNeeded() 
        {
            var DataStore = DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
            if (DataStore.IsLoginNeeded())
            {
                await DataStore.UpdateAuthTokenInHeaderAsync();
            }
        }

    }
}
