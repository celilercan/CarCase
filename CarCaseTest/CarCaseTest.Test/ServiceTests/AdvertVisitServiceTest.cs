using CarCaseTest.Business.Services;
using CarCaseTest.Domain.Entities;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Models.AdvertVisits;
using CarCaseTest.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CarCaseTest.Test.ServiceTests
{
    [TestFixture]
    public class AdvertVisitServiceTest
    {
        private readonly Mock<IAdvertVisitRepository> _advertVisitRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public AdvertVisitServiceTest()
        {
            _advertVisitRepositoryMock = new Mock<IAdvertVisitRepository>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task Should_Return_Success_When_Valid_Advert_Visit_Save()
        {
            var advertVisitModel = new AddAdvertVisitModel { AdvertId = 14930850, IPAddress = "127.0.0.1", VisitDate = DateTime.Now };
            var advertVisit = new AdvertVisitHistory { AdvertId = 14930850, IPAddress = "127.0.0.1", VisitDate = DateTime.Now };
            _advertVisitRepositoryMock.Setup(x => x.AddAsync(advertVisit));
            var advertVisitService = new AdvertVisitService(_advertVisitRepositoryMock.Object, _configurationMock.Object);
            var result = await advertVisitService.AddVisit(advertVisitModel);
            Assert.AreEqual(result.Status, ResultStatus.Success);
        }
    }
}
