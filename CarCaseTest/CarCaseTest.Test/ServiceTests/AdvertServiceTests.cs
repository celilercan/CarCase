using CarCaseTest.Business.Interfaces;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Models.Adverts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Test.ServiceTests
{
    [TestFixture]
    public class AdvertServiceTests
    {
        private readonly IAdvertService _advertService;

        public AdvertServiceTests(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        [Test]
        public void Should_Return_Success_When_Empty_Filter_Have_Adverts()
        {
            var result = _advertService.SearchAsync(new AdvertSearchFilterModel());
            Assert.AreEqual(result.Status, ResultStatus.Success);
        }

        [Test]
        public void Should_Return_NoContent_When_Wrong_Filter_HaveNo_Adverts()
        {
            var result = _advertService.SearchAsync(new AdvertSearchFilterModel { CategoryId = 0 });
            Assert.AreEqual(result.Status, ResultStatus.NoContent);
        }

        [Test]
        public void Should_Return_Success_When_Advert_Detail_By_Id()
        {
            var advertId = 14930850;
            var result = _advertService.GetAdvertAsync(advertId);
            Assert.AreEqual(result.Status, ResultStatus.Success);
        }

        [Test]
        public void Should_Return_NoContent_When_Wrong_Advert_Id()
        {
            var advertId = 0;
            var result = _advertService.GetAdvertAsync(advertId);
            Assert.AreEqual(result.Status, ResultStatus.NoContent);
        }
    }
}