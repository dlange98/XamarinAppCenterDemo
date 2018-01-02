using NUnit.Framework;
using System;
using Refapp.ViewModels;

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
            Assert.AreEqual("dn was here", x.Title);
        }
    }
}
