using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RefappUITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void LoginTest()
        {
            //todo: Josh A, fill in
            app.WaitForElement(c => c.Css("input#i0116"));
            app.EnterText(c => c.Css("input#i0116"), "jaudibert@cardinalsolutions.com");
            app.PressEnter();

            app.Screenshot("Second Login Screen");
            app.WaitForElement(c => c.Css("#passwordInput"));
            app.EnterText(c => c.Css("#passwordInput"), "Bluedragon1!");
            app.Tap(c => c.Css("#submitButton"));

            AppResult[] results = app.WaitForElement(c => c.Marked("ItemsPage"));
            Assert.IsTrue(results.Any(), "Logged in!");
        }

        [Test]
        public void AddItemTest(){
            //login first
            app.WaitForElement(c => c.Css("input#i0116"));
            app.EnterText(c => c.Css("input#i0116"), "jaudibert@cardinalsolutions.com");
            app.PressEnter();

            app.WaitForElement(c => c.Css("#passwordInput"));
            app.EnterText(c => c.Css("#passwordInput"), "Bluedragon1!");
            app.Tap(c => c.Css("#submitButton"));

            //Add the item
            app.WaitForElement(c => c.Marked("ItemsPage"));
            app.Flash(c => c.Marked("ItemsPage"));
            app.Screenshot("Pre Add Item");
            app.WaitForElement(c => c.Marked("Add"));
            app.Tap(c => c.Marked("Add"));

            app.WaitForElement(c => c.Marked("NameText"));
            app.ClearText(c => c.Marked("NameText"));
            app.EnterText(c => c.Marked("NameText"), "Josh");
            app.ClearText(c => c.Marked("DescriptionText"));
            app.EnterText(c => c.Marked("DescriptionText"), "Is the best jedi");
            app.Tap(c => c.Marked("Save"));

            app.WaitForElement("ItemsPage");
            app.Screenshot("Post Add Item");
            Assert.IsTrue(true);
        }
    }

}
