using CarCaseTest.Domain.Enums;

namespace CarCaseTest.Domain.Models.Adverts
{
    public class AdvertSearchFilterModel
    {
        public AdvertSearchFilterModel()
        {
            this.Page = 1;
            this.PageSize = 10;
        }

        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public GearType? Gear { get; set; }
        public FuelType? Fuel { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public SortType? SortType { get; set; }
    }
}
