using NUnit.Framework;
using System;
using Refapp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Refapp.Services;
using Xamarin.UITest;

namespace RefappUITest
{
    [TestFixture()]
    public class UnitTest
    {
        [Test()]
        public void TestCase()
        {
            var x = new AboutViewModel();

            x.Title = "dan was here";
            Assert.AreEqual("dan was here", x.Title);
        }

        [Test()]
        // Test that a property change on a view model is raising a property changed is raising a 
        // property change event
        public async Task SettingIsBusyPropertyShouldRaisePropertyChanged()
        {
            bool invoked = false;
            var aboutViewModel = new AboutViewModel();

            aboutViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsBusy"))
                    invoked = true;
                Assert.True(true);
            };

            aboutViewModel.IsBusy = true;

            await Task.Delay(500);

            if (invoked == false)
            {
                Assert.True(false);
            }

        }
    }
}
