using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;

namespace WingsOn.WebApi.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IFlightRepository flightRepository;
        private IBookingRepository bookingRepository;
        private ILogger logger;

        public BookingController(
            IFlightRepository flightRepository, 
            IBookingRepository bookingRepository,
            ILogger<BookingController> logger)
        {
            this.flightRepository = flightRepository;
            this.bookingRepository = bookingRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all the passengers of a flight.
        /// </summary>
        /// <param name="flightNumber">The number of the desired flight.</param>
        /// <returns></returns>
        [HttpGet("passengers/{flightNumber}")]
        public ActionResult<IEnumerable<Person>> GetPassengers(string flightNumber)
        {
            Flight flight = flightRepository.GetByFlightNumber(flightNumber);

            if (flight == null)
            {
                logger.LogWarning("Invalid flight number: ", flightNumber);
                return BadRequest("Invalid flight number.");
            }

            List<Person> passengers = new List<Person>();
            IEnumerable<Booking> bookings = bookingRepository.GetBookings(flightNumber);

            foreach (var booking in bookings)
            {
                passengers.AddRange(booking.Passengers);
            }

            return Ok(passengers);
        }
    }
}