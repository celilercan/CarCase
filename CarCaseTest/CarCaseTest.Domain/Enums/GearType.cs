using System.ComponentModel.DataAnnotations;

namespace CarCaseTest.Domain.Enums
{
    public enum GearType
    {
        [Display(Name = "Düz")]
        Manual = 1,

        [Display(Name = "Otomatik")]
        Auto = 2,

        [Display(Name = "Yarı Otomatik")]
        SemiAuto = 3
    }
}
