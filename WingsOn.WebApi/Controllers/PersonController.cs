using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WingsOn.Application.Abstract;
using WingsOn.Domain;

namespace WingsOn.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonAppService personAppService;

        private readonly ILogger logger;

        public PersonController(
            IPersonAppService personAppService,
            ILogger<PersonController> logger)
        {
            this.personAppService = personAppService;
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
            try
            {
                var person = personAppService.Get(id);

                return Ok(person);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Gets all persons with given gender value.
        /// </summary>
        /// <param name="gender">Gender filter</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetByGender(string gender)
        {
            try
            {
                var personList = personAppService.GetByGender(gender);

                return Ok(personList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}