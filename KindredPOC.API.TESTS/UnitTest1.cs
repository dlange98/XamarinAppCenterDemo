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
