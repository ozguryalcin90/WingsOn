using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger logger;

        public PersonController(
            IPersonRepository personRepository,
            ILogger<PersonController> logger)
        {
            this.personRepository = personRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Gets Person with id.
        /// </summary>
        /// <param name="id">The id of the desired Person</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            var person = personRepository.Get(id);

            if (person == null)
            {
                logger.LogWarning("Person is not found. id: {0}", id);
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        /// Gets all persons.
        /// </summary>
        /// <param name="gender">Gender filter</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                return Ok(personRepository.GetAll());
            }

            GenderType genderEnum;

            if (!Enum.TryParse(gender, true, out genderEnum))
            {
                logger.LogWarning("Invalid gender value entered: ", gender);
                return BadRequest("Invalid gender value.");
            }

            return Ok(personRepository.GetByGender(genderEnum));
        }
    }
}