using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Business.Search.IndexModels
{
    public class AdvertSearchResponse
    {
        public long Total { get; set; }
        public IEnumerable<AdvertListIndex> Documents { get; set; }
    }
}
