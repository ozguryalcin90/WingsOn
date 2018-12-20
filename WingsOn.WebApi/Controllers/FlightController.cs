using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;

namespace WingsOn.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private IFlightRepository flightRepository;

        public FlightController(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> Get()
        {
            return Ok(flightRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> Get(int id)
        {
            Flight flight = flightRepository.Get(id);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }
    }
}