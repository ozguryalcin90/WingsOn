using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WingsOn.Dal.Abstract;
using WingsOn.Domain;
using WingsOn.WebApi.Controllers;
using Xunit;

namespace WingsOn.Tests
{
    public class PersonControllerTests
    {
        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<ILogger<PersonController>> mockLogger;

        private PersonController personController;

        public PersonControllerTests()
        {
            mockPersonRepository = new Mock<IPersonRepository>();
            mockLogger = new Mock<ILogger<PersonController>>();

            personController = new PersonController(mockPersonRepository.Object, mockLogger.Object);
        }

        #region Get Method
        [Fact]
        public void Get_IfPersonNotFound_ReturnsNotFound()
        {
            mockPersonRepository.Setup(mock => mock.Get(1)).Returns<StatusCodeResult>(null);

            var result = personController.Get(1).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOk()
        {
            mockPersonRepository.Setup(mock => mock.Get(1)).Returns(new Person());

            var result = personController.Get(1);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsRightPerson()
        {
            var person = new Person() { Id = 1 };
            mockPersonRepository.Setup(mock => mock.Get(1)).Returns(person);

            var result = personController.Get(1).Result as OkObjectResult;
            var resultPerson = Assert.IsType<Person>(result.Value);

            Assert.Equal(person, resultPerson);
        }
        #endregion Get Method

        #region GetByGender Method
        [Fact]
        public void GetByGender_IfGenderNotValid_ReturnsBadRequest()
        {
            var result = personController.GetByGender("invalidGender");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetByGender_WhenCalled_ReturnsOk()
        {
            var result = personController.GetByGender("female");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetByGender_WhenCalled_ReturnsRightAmountOfMales()
        {
            var mockPersons = new List<Person> { new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Female } };
            mockPersonRepository.Setup(mock => mock.GetByGender(GenderType.Male)).Returns(mockPersons);

            var result = personController.GetByGender("male").Result as OkObjectResult;
            var persons = Assert.IsType<List<Person>>(result.Value);

            Assert.Equal(2, persons.Where(person => person.Gender == GenderType.Male).Count());
        }

        [Fact]
        public void GetByGender_WhenCalled_ReturnsRightAmountOfFemales()
        {
            var mockPersons = new List<Person> { new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Female } };
            mockPersonRepository.Setup(mock => mock.GetByGender(GenderType.Female)).Returns(mockPersons);

            var result = personController.GetByGender("female").Result as OkObjectResult;
            var persons = Assert.IsType<List<Person>>(result.Value);

            Assert.Equal(1, persons.Where(person => person.Gender == GenderType.Female).Count());
        }
        #endregion GetByGender Method
    }
}