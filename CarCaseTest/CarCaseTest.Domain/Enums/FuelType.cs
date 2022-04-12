using System.ComponentModel.DataAnnotations;

namespace CarCaseTest.Domain.Enums
{
    public enum FuelType
    {
        [Display(Name = "Benzin")]
        Gasoline = 1,

        [Display(Name = "Dizel")]
        FuelOil = 2,

        [Display(Name = "LPG & Benzin")]
        GasAndGasoline = 3
    }
}
