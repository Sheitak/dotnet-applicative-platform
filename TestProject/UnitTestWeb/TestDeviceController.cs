using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAppMVC.Controllers;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace TestProject.UnitTestWeb
{
    public class TestDeviceController
    {
        [Fact]
        public async Task TestDeviceGetDetailsValid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var deviceRepositoryMock = new Mock<DeviceRepository>();
            deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new Device()); // Remplacez par une instance appropriée de Device

            var controller = new DeviceController(loggerMock.Object, deviceRepositoryMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await controller.Details(1); // Remplacez par l'ID approprié

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task TestDeviceGetDetailsInvalid() {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var deviceRepositoryMock = new Mock<DeviceRepository>();
            deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((Device)null);

            var controller = new DeviceController(loggerMock.Object, deviceRepositoryMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await controller.Details(1); // Remplacez par l'ID approprié

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

        [Fact]
        public async Task TestDevicePutDetailsValid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var deviceRepositoryMock = new Mock<DeviceRepository>();
            deviceRepositoryMock.Setup(repo => repo.PutDevice(It.IsAny<Device>(), It.IsAny<string>()))
                .ReturnsAsync((Device)null); // Remplacez par la valeur appropriée

            var controller = new DeviceController(loggerMock.Object, deviceRepositoryMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var device = new Device(); // Remplacez par une instance appropriée de Device

            // Act
            var result = await controller.Edit(device, true); // Remplacez par les valeurs appropriées

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            Assert.Equal(device.DeviceID, redirectToActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task TestDevicePutDetailsInvalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var deviceRepositoryMock = new Mock<DeviceRepository>();
            deviceRepositoryMock.Setup(repo => repo.PutDevice(It.IsAny<Device>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error message")); // Remplacez par le comportement approprié

            var controller = new DeviceController(loggerMock.Object, deviceRepositoryMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var device = new Device(); // Remplacez par une instance appropriée de Device

            // Act
            var result = await controller.Edit(device, true); // Remplacez par les valeurs appropriées

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}
