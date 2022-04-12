using System.ComponentModel.DataAnnotations;

namespace CarCaseTest.Domain.Enums
{
    public enum ColorType
    {
        [Display(Name = "Diğer")]
        None = 0,

        [Display(Name = "Altın")]
        Gold = 1,

        [Display(Name = "Bej")]
        Beige = 2,

        [Display(Name = "Beyaz")]
        White = 3,

        [Display(Name = "Bordo")]
        Burgundy = 4,

        [Display(Name = "Füme")]
        Smoky = 5,

        [Display(Name = "Gri")]
        Grey = 6,

        [Display(Name = "Gri (Gümüş)")]
        Silver = 7,

        [Display(Name = "Gri (metalik)")]
        Metallic = 8,

        [Display(Name = "Gri (titanyum)")]
        Titanium = 9,

        [Display(Name = "Kırmızı")]
        Red = 10,

        [Display(Name = "Kahverengi")]
        Brown = 11,

        [Display(Name = "Lacivert")]
        DarkBlue = 12,

        [Display(Name = "Mavi")]
        Blue = 13,

        [Display(Name = "Siyah")]
        Black = 14
    }
}
