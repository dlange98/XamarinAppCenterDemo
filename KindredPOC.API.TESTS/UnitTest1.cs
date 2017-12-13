using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KindredPOC.API.TESTS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SaveItemFillsInKey()
        {
            //Assert
            string LocalCon = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=KindredPOC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            KindredPOC.API.Models.DataRepository repo = new Models.DataRepository(LocalCon);
            //Act
            Models.Item savedItem = repo.CreateItem(new Models.Item { Text = "TEST Item 1", Description = "Test Description 1" });
            //Assess
            Assert.IsNotNull(savedItem);
            Assert.IsNotNull(savedItem.Id);
            Assert.IsTrue(savedItem.Id.Length > 0);

        }
        [TestMethod]
        public void UpdatedItemsKeepSameKey()
        {
            //Assert
            string LocalCon = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=KindredPOC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            KindredPOC.API.Models.DataRepository repo = new Models.DataRepository(LocalCon);
            //Act
            Models.Item savedItem = repo.CreateItem(new Models.Item { Text = "TEST Item 1", Description = "Test Description 1" });
            Models.Item UpdateItem = savedItem;
            UpdateItem.Text = $"TEST Item 1 Update {DateTime.Now.ToShortTimeString()}";
            UpdateItem.Description = $"Test Description 1 Update { DateTime.Now.ToShortTimeString()}";
            var returnitem = repo.UpdateItem(UpdateItem);

            //Assess
            Assert.IsNotNull(UpdateItem);
            Assert.IsNotNull(UpdateItem.Id);
            Assert.IsTrue(savedItem.Id.Equals(returnitem.Id));

        }
    }
}
