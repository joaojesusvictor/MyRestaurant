using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers;
using MyRestaurant.Api.Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void GetOrder_In_NonExistentArea()
        {
            //Arrange
            string area = "FRAI";
            //Using Nuget Moq to create an OrderDbContext simulation
            var mockDbContext = new Mock<OrderDbContext>();
            var controller = new OrderController(mockDbContext.Object);

            //Act
            var result = controller.Get(area);

            //Asset
            //Getting the return statuscode
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Theory]
        [InlineData("Big Mac", "grill")]
        [InlineData("Big Coke", "DRINk")]
        [InlineData("Big Fries", "Fries")]
        [InlineData("Small Fries", "fris")]
        public void AddThreeOrders_Successfull_And_OneFail(string order, string area)
        {
            //Arrange
            //Using Nuget Moq to create an OrderDbContext simulation
            var mockDbContext = new Mock<OrderDbContext>();
            var controller = new OrderController(mockDbContext.Object);

            //Act
            var result = controller.AddOrder(order, area);

            //Asset
            //Getting the return statuscode
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteOrder_Successfull()
        {
            //Arrange
            string area = "Fries";
            //Using Nuget Moq to create an OrderDbContext simulation
            var mockDbContext = new Mock<OrderDbContext>();
            var controller = new OrderController(mockDbContext.Object);

            //Act
            var result = controller.DeleteOrder(area);

            //Asset
            //Getting the return statuscode
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
