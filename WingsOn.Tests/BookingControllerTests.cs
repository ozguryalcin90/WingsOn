using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;
using WingsOn.WebApi.Controllers;
using Xunit;

namespace WingsOn.Tests
{
    public class BookingControllerTests
    {
        private Mock<IFlightRepository> mockFlightRepository;
        private Mock<IBookingRepository> mockBookingRepository;

        private BookingController bookingController;

        public BookingControllerTests()
        {
            mockFlightRepository = new Mock<IFlightRepository>();
            mockBookingRepository = new Mock<IBookingRepository>();

            bookingController = new BookingController(mockFlightRepository.Object, mockBookingRepository.Object);
        }

        #region GetPassengers Method
        [Fact]
        public void GetPassengers_IfFlightNotFound_ReturnsNotFound()
        {
            mockFlightRepository.Setup(mock => mock.GetByFlightNumber("123")).Returns<StatusCodeResult>(null);

            var result = bookingController.GetPassengers("123");
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetPassengers_WhenCalled_ReturnsOk()
        {
            Flight mockFlight = new Flight
            {
                Number = "123"
            };

            mockFlightRepository.Setup(mock => mock.GetByFlightNumber("123")).Returns(mockFlight);

            var result = bookingController.GetPassengers("123");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPassengers_WhenCalled_ReturnsRightPassengers()
        {
            Flight mockFlight = new Flight
            {
                Number = "123"
            };

            mockFlightRepository.Setup(mock => mock.GetByFlightNumber("123")).Returns(mockFlight);

            List<Booking> mockBookings = new List<Booking>
            {
                new Booking {
                    Flight = mockFlight,
                    Passengers = new List<Person>
                    {
                        new Person(),
                        new Person()
                    }
                }
            };

            mockBookingRepository.Setup(mock => mock.GetBookings("123")).Returns(mockBookings);

            var result = bookingController.GetPassengers("123").Result as OkObjectResult;
            List<Person> passengers = Assert.IsType<List<Person>>(result.Value);

            Assert.Equal(2, passengers.Count);
        }
        #endregion GetPassengers Method
    }
}
