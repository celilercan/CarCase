using CarCaseTest.Business.Search;
using CarCaseTest.Business.Services;
using CarCaseTest.Domain.Entities;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CarCaseTest.Test.ServiceTests
{
    [TestFixture]
    public class AdvertServiceTests
    {
        private readonly Mock<IAdvertRepository> _advertRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public AdvertServiceTests()
        {
            _advertRepositoryMock = new Mock<IAdvertRepository>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task Should_Return_Success_When_Advert_Detail_By_Id()
        {
            var advertId = 14930850;
            var resultAdvert = new Advert { Id = advertId };
            var elasticSearchManager = new ElasticSearchManager("http://elasticsearch:9200");
            _advertRepositoryMock.Setup(x => x.GetByIdAsync(advertId)).Returns(Task.FromResult(resultAdvert));
            var advertService = new AdvertService(_advertRepositoryMock.Object, elasticSearchManager, _configurationMock.Object);
            var result = await advertService.GetAdvertAsync(advertId);
            Assert.AreEqual(result.Status, ResultStatus.Success);
        }

        [Test]
        public async Task Should_Return_NoContent_When_Wrong_Advert_Id()
        {
            var advertId = 0;
            var elasticSearchManager = new ElasticSearchManager("http://elasticsearch:9200");
            var advertService = new AdvertService(_advertRepositoryMock.Object, elasticSearchManager, _configurationMock.Object);
            var result = await advertService.GetAdvertAsync(advertId);
            Assert.AreEqual(result.Status, ResultStatus.NoContent);
        }
    }
}