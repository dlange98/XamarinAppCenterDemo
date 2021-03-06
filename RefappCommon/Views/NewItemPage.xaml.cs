﻿using System;
using Refapp.Models;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;

namespace Refapp
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            Analytics.TrackEvent("Add New Item");
            await Navigation.PopToRootAsync();
        }
    }
}
