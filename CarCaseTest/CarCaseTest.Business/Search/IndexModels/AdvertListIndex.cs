using Nest;
using System;

namespace CarCaseTest.Business.Search.IndexModels
{
    public class AdvertListIndex
    {
        public int Id { get; set; }

        [Text(Analyzer = "turkish_analyzer")]
        public string ModelName { get; set; }

        [Text(Analyzer = "turkish_analyzer")]
        public string Category { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        [Text(Analyzer = "turkish_analyzer")]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public int Km { get; set; }

        public int ColorId { get; set; }

        public int GearId { get; set; }

        public int FuelId { get; set; }
    }
}
