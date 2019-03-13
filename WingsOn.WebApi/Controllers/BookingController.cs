using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WingsOn.Application.Abstract;
using WingsOn.Application.Utility;
using WingsOn.Domain;

namespace WingsOn.WebApi.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingAppService bookingAppService;

        private readonly ILogger logger;

        public BookingController(
            IBookingAppService bookingAppService,
            ILogger<BookingController> logger)
        {
            this.bookingAppService = bookingAppService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all the passengers of a flight.
        /// </summary>
        /// <param name="flightNumber">The number of the desired flight.</param>
        /// <returns></returns>
        [HttpGet("passengers/flights/{flightNumber}")]
        public ActionResult<IEnumerable<Person>> Get(string flightNumber)
        {
            try
            {
                var passengers = bookingAppService.Get(flightNumber);

                return Ok(passengers);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}