using System;
using System.Collections.Generic;
using WingsOn.Application.Abstract;
using WingsOn.Application.Utility;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;

namespace WingsOn.Application
{
    public class PersonAppService : IPersonAppService
    {
        private IPersonRepository personRepository;

        public PersonAppService(
            IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public Person Get(int id)
        {
            var person = personRepository.Get(id);

            if (person == null)
            {
                throw new EntityNotFoundException(id.ToString());
            }

            return person;
        }

        public IEnumerable<Person> GetByGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                throw new ArgumentNullException(gender);
            }

            GenderType genderEnum;

            if (!Enum.TryParse(gender, true, out genderEnum))
            {
                throw new ArgumentException("Gender value is invalid.");
            }

            return personRepository.GetByGender(genderEnum);
        }
    }
}