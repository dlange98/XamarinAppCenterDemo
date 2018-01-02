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
        public async Task SettingOrderPropertyShouldRaisePropertyChanged()
        {
            bool invoked = false;
            var baseViewModel = new AboutViewModel();

            baseViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsBusy"))
                    invoked = true;
                Assert.True(true);
            };

            baseViewModel.IsBusy = true;

            await Task.Delay(500);

            if (invoked == false)
            {
                Assert.True(false);
            }

        }
    }
}
