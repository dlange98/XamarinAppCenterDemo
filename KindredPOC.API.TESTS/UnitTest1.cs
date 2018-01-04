using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace KindredPOC.API.TESTS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public async Task negative_take_throws_exception()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            //Act
            
            var items = await bl.GetDataRepoItems(repo,-3, 0);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task BL_Update_Fails_with_null_input()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            //Act
            KindredPOC.API.Models.Item item = null;// new Models.Item { Id = Guid.NewGuid().ToString(), Description = "Test Description", Text = "Test Text" };
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var isSuccess = await bl.UpdateItem(item, sw);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task BL_Update_Fails_with_null_input2()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            //Act
            KindredPOC.API.Models.Item item = new Models.Item { Id = Guid.NewGuid().ToString(), Description = "Test Description", Text = "Test Text" };
            Stopwatch sw = null;
            var isSuccess = await bl.UpdateItem(item, sw);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task BL_Delete_Fails_with_null_input()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            string Id = null;
            Stopwatch sw = new Stopwatch();
            //Act

            var isSuccess = await bl.DeleteItem(Id, sw);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task BL_Delete_Fails_with_null_input2()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            string Id = Guid.NewGuid().ToString();
            Stopwatch sw = null;
            //Act
            var isSuccess = await bl.DeleteItem(Id, sw);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task BL_Save_Fails_with_null_input()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            KindredPOC.API.Models.Item item = new Models.Item { Id = Guid.NewGuid().ToString(), Description = "Test Description", Text = "Test Text" };
            Stopwatch sw = null;
            //Act

            var savedItem = await bl.SaveItem(item, sw);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task BL_Save_Fails_with_null_input2()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            KindredPOC.API.Models.Item item = null;
            Stopwatch sw = new Stopwatch();
            //Act
            var isSuccess = await bl.SaveItem(item, sw);
            //Assess
            //notused

        }
        [TestMethod]
        public async Task BL_Save_returns_Item_with_Id()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            KindredPOC.API.Models.Item item = new Models.Item { Id = "", Description = "Test Description", Text = "Test Text" };
            Stopwatch sw = new Stopwatch();
            //Act
            var saveditem = await bl.SaveItem(item, sw);
            //Assess
            Assert.IsNotNull(saveditem);
            Assert.IsTrue(saveditem.Id.Length > 0);
        }
        //[TestMethod]
        //[ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        //public async Task BL_Save_returns_object()
        //{
        //    //Assert
        //    KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
        //    Code.BusinessLayer bl = new Code.BusinessLayer(repo);
        //    //Act

        //    var items = await bl.SaveItem(new HttpRequestMessage(),new Microsoft, repo, -3, 0);
        //    //Assess
        //    //notused

        //}
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public async Task negative_Skip_throws_exception()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            //Act

            var items = await bl.GetDataRepoItems(repo, 3,-3);
            //Assess
            //notused

        }
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public async Task Null_Repo_throws_exception()
        {
            //Assert
            KindredPOC.API.Models.IDataRepository repo = null;
            Code.BusinessLayer bl = new Code.BusinessLayer(repo);
            //Act

            var items = await bl.GetDataRepoItems(repo, 3, -3);
            //Assess
            //notused

        }

    }
}
