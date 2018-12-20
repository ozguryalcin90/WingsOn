using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;

namespace WingsOn.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonRepository personRepository;
        private IBookingRepository bookingRepository;
        private IFlightRepository flightRepository;

        public PersonController(IPersonRepository personRepository, IBookingRepository bookingRepository, IFlightRepository flightRepository)
        {
            this.personRepository = personRepository;
            this.bookingRepository = bookingRepository;
            this.flightRepository = flightRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            Person person = personRepository.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet("gender/{gender}")]
        public ActionResult<IEnumerable<Person>> GetByGender(string gender)
        {
            GenderType genderEnum;

            if (!Enum.TryParse(gender, true, out genderEnum))
            {
                return NotFound();
            }

            return Ok(personRepository.GetByGender(genderEnum));
        }

        [HttpGet("flight/{flightNumber}")]
        public ActionResult<IEnumerable<Person>> GetPassengers(string flightNumber)
        {
            Flight flight = flightRepository.GetByFlightNumber(flightNumber);

            if (flight == null)
            {
                return NotFound();
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