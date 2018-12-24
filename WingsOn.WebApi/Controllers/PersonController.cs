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

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
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
                return BadRequest("Invalid gender value.");
            }

            return Ok(personRepository.GetByGender(genderEnum));
        }
    }
}