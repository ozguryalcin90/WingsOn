using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using WingsOn.Application.Abstract;
using WingsOn.Application.Utility;
using WingsOn.Domain;
using WingsOn.WebApi.Controllers;
using Xunit;

namespace WingsOn.Tests
{
    public class BookingControllerTests
    {
        private Mock<IBookingAppService> mockBookingAppService;
        private Mock<ILogger<BookingController>> mockLogger;

        private BookingController bookingController;

        public BookingControllerTests()
        {
            mockBookingAppService = new Mock<IBookingAppService>();
            mockLogger = new Mock<ILogger<BookingController>>();

            bookingController = new BookingController(mockBookingAppService.Object, mockLogger.Object);
        }

        #region GetPassengers Method
        [Fact]
        public void GetPassengers_IfFlightNotFound_ReturnsBadRequest()
        {
            mockBookingAppService.Setup(mock => mock.Get("123")).Throws<EntityNotFoundException>();

            var result = bookingController.Get("123");
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetPassengers_WhenCalled_ReturnsOk()
        {
            Flight mockFlight = new Flight
            {
                Number = "123"
            };

            mockBookingAppService.Setup(mock => mock.Get("123")).Returns(new List<Person>());

            var result = bookingController.Get("123");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPassengers_WhenCalled_ReturnsRightPassengers()
        {
            var mockPassengers = new List<Person>()
            {
                new Person(){Id = 1},
                new Person(){Id = 2}
            };

            mockBookingAppService.Setup(mock => mock.Get("123")).Returns(mockPassengers);

            var result = bookingController.Get("123").Result as OkObjectResult;
            var passengers = Assert.IsType<List<Person>>(result.Value);

            Assert.Equal(2, passengers.Count);
        }
        #endregion GetPassengers Method
    }
}
