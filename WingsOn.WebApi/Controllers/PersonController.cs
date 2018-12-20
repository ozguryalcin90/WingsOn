using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(personRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            return Ok(personRepository.Get(id));
        }

        //// POST api/person
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/person/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/person/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
