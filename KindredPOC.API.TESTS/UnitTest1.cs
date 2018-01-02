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
        //[TestMethod]
        //[ExpectedException(typeof(System.ArgumentNullException))]
        //public async Task Null_Input_toBL_Save_throws_exception()
        //{
        //    //Assert
        //    KindredPOC.API.Models.IDataRepository repo = new Models.MockDataRepo();
        //    Code.BusinessLayer bl = new Code.BusinessLayer(repo);
        //    HttpRequestMessage Moqreq = new HttpRequestMessage();
        //    TraceWriter MoqLog = new MoqTraceWriter(TraceLevel.Info);
        //    Stopwatch moqwatch = new Stopwatch();
        //    //Act

        //    var items = await bl.SaveItem(Moqreq, MoqLog, moqwatch);
        //    //Assess
        //    //notused

        //}
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

    //public class MoqTraceWriter : TraceWriter
    //{
    //    public MoqTraceWriter(TraceLevel level) : base(level)
    //    {
    //    }

    //    public override void Trace(TraceEvent traceEvent)
    //    {
    //        //does nothing
    //    }
    //}
}
