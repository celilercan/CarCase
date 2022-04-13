using CarCaseTest.Business.Interfaces;
using CarCaseTest.Domain.Models.AdvertVisits;
using CarCaseTest.Queue.Events;
using MassTransit;
using System.Threading.Tasks;

namespace CarCaseTest.Queue.Consumers
{
    public class AdvertVisitConsumer : IConsumer<AdvertVisitMessage>
    {
        private readonly IAdvertVisitService _advertVisitService;

        public AdvertVisitConsumer(IAdvertVisitService advertVisitService)
        {
            _advertVisitService = advertVisitService;
        }

        public Task Consume(ConsumeContext<AdvertVisitMessage> context)
        {
            var advertVisit = new AddAdvertVisitModel
            {
                AdvertId = context.Message.AdvertId,
                IPAddress = context.Message.IPAddress,
                VisitDate = context.Message.VisitDate
            };

            _advertVisitService.AddVisit(advertVisit);

            return Task.CompletedTask;
        }
    }
}
