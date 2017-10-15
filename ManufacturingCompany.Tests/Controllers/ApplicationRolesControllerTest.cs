using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManufacturingCompany.Controllers.AdminController;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ManufacturingCompany.Tests.Controllers
{
    [TestClass]
    public class ApplicationRolesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ApplicationRolesController controller = new ApplicationRolesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
