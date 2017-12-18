using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refapp.Models;
using Refapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.AppCenter.Analytics;

namespace Refapp
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
  
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            this.AutomationId = "ItemsPage";

            if (Device.RuntimePlatform == Device.iOS)
            {
                On<iOS>().SetUseSafeArea(true);
            }

            Analytics.TrackEvent("View items page");
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
