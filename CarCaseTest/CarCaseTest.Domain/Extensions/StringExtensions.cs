using CarCaseTest.Domain.Enums;

namespace CarCaseTest.Domain.Extensions
{
    public static class StringExtensions
    {
        public static int ToEnumId(this string str)
        {
            return str switch
            {
                "Düz" => (int)GearType.Manual,
                "Yarı Otomatik" => (int)GearType.SemiAuto,
                "Otomatik" => (int)GearType.Auto,

                "Benzin" => (int)FuelType.Gasoline,
                "Dizel" => (int)FuelType.FuelOil,
                "LPG & Benzin" => (int)FuelType.GasAndGasoline,

                "Bej" => (int)ColorType.Beige,
                "Altın" => (int)ColorType.Gold,
                "Beyaz" => (int)ColorType.White,
                "Bordo" => (int)ColorType.Burgundy,
                "Füme" => (int)ColorType.Smoky,
                "Gri (Gümüş)" => (int)ColorType.Silver,
                "Gri" => (int)ColorType.Grey,
                "Gri (metalik)" => (int)ColorType.Metallic,
                "Gri (titanyum)" => (int)ColorType.Titanium,
                "Kırmızı" => (int)ColorType.Red,
                "Kahverengi" => (int)ColorType.Brown,
                "Lacivert" => (int)ColorType.DarkBlue,
                "Mavi" => (int)ColorType.Blue,
                "Siyah" => (int)ColorType.Black,

                _ => default(int),
            };
        }
    }
}
