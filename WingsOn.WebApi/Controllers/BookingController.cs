using Microsoft.AspNetCore.Mvc;
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

        public BookingController(IFlightRepository flightRepository, IBookingRepository bookingRepository)
        {
            this.flightRepository = flightRepository;
            this.bookingRepository = bookingRepository;
        }

        [HttpGet("passengers/{flightNumber}")]
        public ActionResult<IEnumerable<Person>> GetPassengers(string flightNumber)
        {
            Flight flight = flightRepository.GetByFlightNumber(flightNumber);

            if (flight == null)
            {
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