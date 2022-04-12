using CarCaseTest.Business.Interfaces;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Models.AdvertVisits;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Test.ServiceTests
{
    [TestFixture]
    public class AdvertVisitServiceTest
    {
        private readonly IAdvertVisitService _advertVisitService;

        public AdvertVisitServiceTest(IAdvertVisitService advertVisitService)
        {
            _advertVisitService = advertVisitService;
        }

        [Test]
        public void Should_Return_Success_When_Valid_Advert_Visit_Save()
        {
            var advertVisit = new AddAdvertVisitModel { AdvertId = 14930850, IPAddress = "127.0.0.1", VisitDate = DateTime.Now };
            var result = _advertVisitService.AddVisit(advertVisit);
            Assert.AreEqual(result.Status, ResultStatus.Success);
        }
    }
}
