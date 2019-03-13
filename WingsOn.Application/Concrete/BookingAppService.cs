using System.Collections.Generic;
using WingsOn.Application.Abstract;
using WingsOn.Application.Utility;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;

namespace WingsOn.Application
{
    public class BookingAppService : IBookingAppService
    {
        private IFlightRepository flightRepository;
        private IBookingRepository bookingRepository;

        public BookingAppService(
            IFlightRepository flightRepository,
            IBookingRepository bookingRepository)
        {
            this.flightRepository = flightRepository;
            this.bookingRepository = bookingRepository;
        }

        public IEnumerable<Person> Get(string flightNumber)
        {
            var flight = flightRepository.GetByFlightNumber(flightNumber);

            if (flight == null)
            {
                throw new EntityNotFoundException(flightNumber);
            }

            var passengers = new List<Person>();
            IEnumerable<Booking> bookings = bookingRepository.GetBookings(flightNumber);

            foreach (var booking in bookings)
            {
                passengers.AddRange(booking.Passengers);
            }

            return passengers;
        }
    }
}