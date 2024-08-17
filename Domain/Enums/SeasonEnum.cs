
using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum SeasonEnum : int
    {
        [Display(Description = "بهار")]
        Spring = 1,

        [Display(Description = "تابستان")]
        Summer = 2,

        [Display(Description = "پاییز")]
        Autumn = 3,

        [Display(Description = "زمستان")]
        Winter = 4,
    }
}
