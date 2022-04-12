using System.Collections.Generic;

namespace CarCaseTest.Domain.Models.Adverts
{
    public class AdvertListResultModel
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public IEnumerable<AdvertListModel> Adverts { get; set; }
    }
}
