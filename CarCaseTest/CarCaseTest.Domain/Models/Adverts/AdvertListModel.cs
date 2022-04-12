using System;

namespace CarCaseTest.Domain.Models.Adverts
{
    public class AdvertListModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string Category { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Km { get; set; }
        public string Color { get; set; }
        public string Gear { get; set; }
        public string Fuel { get; set; }
    }
}
