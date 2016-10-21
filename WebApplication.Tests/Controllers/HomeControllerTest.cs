using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication;
using WebApplication.Controllers;
using System.Web;
using Moq;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        private IHttpContextFactory _httpContextFactory;

        [TestInitialize]
        public void Setup()
        {
            var mockContext = new Mock<IHttpContextFactory>();
            mockContext.Setup(x => x.Create()).Returns(new FakeHttpContext("domain\\TestUser"));
            _httpContextFactory = mockContext.Object;
        }



        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_httpContextFactory);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(_httpContextFactory);

            // Act
            ViewResult result = controller.About() as ViewResult;
            var vm = result.Model as FancyButtonGroupViewModel;

            // Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual("Hello, TestUser.  You currently have this role: user", vm.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(_httpContextFactory);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UserGetsNonAdminButtonsForTheIndexAction()
        {
            //Arrange          
            var controller = new HomeController(_httpContextFactory);

            //Act
            var result = controller.Index() as ViewResult;
            var vm = result.Model as ButtonGroupViewModel;

            //Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual(2, vm.Buttons.Count);
            Assert.AreEqual("Button 2", vm.Buttons.Last());

        }

        [TestMethod]
        public void AdminUserGetsAllButtonsForTheIndexAction()
        {
            //Arrange
            //need to override the FakeContext to pass correct user 
            var mockContext = new Mock<IHttpContextFactory>();
            mockContext.Setup(x => x.Create()).Returns(new FakeHttpContext("MAINPC\\Bryan"));
            
            var controller = new HomeController(mockContext.Object);

            //Act
            var result = controller.Index() as ViewResult;
            var vm = result.Model as ButtonGroupViewModel;

            //Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual(4, vm.Buttons.Count);
            Assert.AreEqual("Secret Button", vm.Buttons.Last());

        }

        [TestMethod]
        public void UserGetsNonAdminButtonsForTheAboutAction()
        {
            //Arrange          
            var controller = new HomeController(_httpContextFactory);

            //Act
            var result = controller.About() as ViewResult;
            var vm = result.Model as FancyButtonGroupViewModel;

            //Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual(1, vm.Buttons.Count);
            Assert.AreEqual("Button 1", vm.Buttons.Last().Name);
            Assert.AreEqual("btn-default", vm.Buttons.Last().Style);

        }

        [TestMethod]
        public void AdminUserGetsAllButtonsForTheAboutAction()
        {
            //Arrange
            //need to override the FakeContext to pass correct user 
            var mockContext = new Mock<IHttpContextFactory>();
            mockContext.Setup(x => x.Create()).Returns(new FakeHttpContext("MAINPC\\Bryan"));

            var controller = new HomeController(mockContext.Object);

            //Act
            var result = controller.About() as ViewResult;
            var vm = result.Model as FancyButtonGroupViewModel;

            //Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual(6, vm.Buttons.Count);
            Assert.AreEqual("Danger", vm.Buttons.Last().Name);
            Assert.AreEqual("btn-danger", vm.Buttons.Last().Style);

        }


    }
}
