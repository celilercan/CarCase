using System;

namespace CarCaseTest.Domain.Entities
{
    public class AdvertVisitHistory : BaseEntity
    {
        public int AdvertId { get; set; }
        public string IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
