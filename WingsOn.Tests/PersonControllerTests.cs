﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WingsOn.Application.Abstract;
using WingsOn.Application.Utility;
using WingsOn.Domain;
using WingsOn.WebApi.Controllers;
using Xunit;

namespace WingsOn.Tests
{
    public class PersonControllerTests
    {
        private Mock<IPersonAppService> mockPersonAppService;
        private Mock<ILogger<PersonController>> mockLogger;

        private PersonController personController;

        public PersonControllerTests()
        {
            mockPersonAppService = new Mock<IPersonAppService>();
            mockLogger = new Mock<ILogger<PersonController>>();

            personController = new PersonController(mockPersonAppService.Object, mockLogger.Object);
        }

        #region Get Method
        [Fact]
        public void Get_IfPersonNotFound_ReturnsNotFound()
        {
            mockPersonAppService.Setup(mock => mock.Get(1)).Throws<EntityNotFoundException>();

            var result = personController.Get(1).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOk()
        {
            mockPersonAppService.Setup(mock => mock.Get(1)).Returns(new Person());

            var result = personController.Get(1);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsRightPerson()
        {
            var person = new Person() { Id = 1 };
            mockPersonAppService.Setup(mock => mock.Get(1)).Returns(person);

            var result = personController.Get(1).Result as OkObjectResult;
            var resultPerson = Assert.IsType<Person>(result.Value);

            Assert.Equal(person, resultPerson);
        }
        #endregion Get Method

        #region GetByGender Method
        [Fact]
        public void GetAll_IfGenderIsMissing_ReturnsBadRequest()
        {
            mockPersonAppService.Setup(mock => mock.GetByGender("")).Throws<ArgumentNullException>();

            var result = personController.GetByGender("");
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetAll_IfGenderNotValid_ReturnsBadRequest()
        {
            mockPersonAppService.Setup(mock => mock.GetByGender("invalidGender")).Throws<ArgumentNullException>();

            var result = personController.GetByGender("invalidGender");
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOk()
        {
            var result = personController.GetByGender("female");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAll_WhenCalledWithGender_ReturnsRightAmountOfMales()
        {
            var mockPersons = new List<Person> { new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Female } };
            mockPersonAppService.Setup(mock => mock.GetByGender("male")).Returns(mockPersons);

            var result = personController.GetByGender("male").Result as OkObjectResult;
            var persons = Assert.IsType<List<Person>>(result.Value);

            Assert.Equal(2, persons.Where(person => person.Gender == GenderType.Male).Count());
        }

        [Fact]
        public void GetAll_WhenCalledWithGender_ReturnsRightAmountOfFemales()
        {
            var mockPersons = new List<Person> { new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Male }, new Person { Gender = GenderType.Female } };
            mockPersonAppService.Setup(mock => mock.GetByGender("female")).Returns(mockPersons);

            var result = personController.GetByGender("female").Result as OkObjectResult;
            var persons = Assert.IsType<List<Person>>(result.Value);

            Assert.Equal(1, persons.Where(person => person.Gender == GenderType.Female).Count());
        }
        #endregion GetByGender Method
    }
}