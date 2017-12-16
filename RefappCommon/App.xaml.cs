using System;
using System.Threading.Tasks;
using Refapp.DAO;
using Refapp.Configuration;
using Refapp.Services;
using Xamarin.Forms;
using Refapp.Managers;
using Refapp.Models;

namespace Refapp
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;
        public AccessTokenDAO TokenDAO;

        public App()
        {
            InitializeComponent();

            var fileManager = DependencyService.Get<IFileManager>();
            TokenDAO = new AccessTokenDAO(fileManager);

          


            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
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

        }


    }
}
