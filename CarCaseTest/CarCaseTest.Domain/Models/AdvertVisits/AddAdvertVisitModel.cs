using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Domain.Models.AdvertVisits
{
    public class AddAdvertVisitModel
    {
        public int AdvertId { get; set; }
        public string IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
