using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAppMVC.Controllers;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace TestProject
{
    public class UnitTestWeb
    {
        [Fact]
        public void TestIndexAction_ReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var studentRepositoryMock = new Mock<StudentRepository>();
            var controller = new StudentController(loggerMock.Object, studentRepositoryMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task TestDetailsAction_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var studentRepositoryMock = new Mock<StudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetStudent(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new Student());

            var controller = new StudentController(loggerMock.Object, studentRepositoryMock.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task TestDetailsAction_WithInvalidId_ReturnsErrorView()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var studentRepositoryMock = new Mock<StudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetStudent(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((Student)null);

            var controller = new StudentController(loggerMock.Object, studentRepositoryMock.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}
