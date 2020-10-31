using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication_mc_02.Controllers;

namespace StoreTests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new ProductController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);

        }
    }
}