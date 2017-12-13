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


        }

        protected override void OnStart()
        {
            base.OnStart();
            TokenDAO.DeleteToken();
        }


    }
}
