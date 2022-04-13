using System;

namespace CarCaseTest.Queue.Events
{
    public class AdvertVisitMessage
    {
        public int AdvertId { get; set; }
        public string IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
