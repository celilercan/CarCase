using CarCaseTest.Domain.Enums;

namespace CarCaseTest.Domain.Models.Adverts
{
    public class AdvertSearchFilterModel
    {
        public AdvertSearchFilterModel()
        {
            this.Page = 1;
        }

        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public GearType? Gear { get; set; }
        public FuelType? Fuel { get; set; }
        public int? Page { get; set; }
        public SortType? SortType { get; set; }
    }
}
