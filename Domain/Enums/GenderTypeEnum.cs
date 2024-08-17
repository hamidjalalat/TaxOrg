
using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum GenderTypeEnum : int
    {
        [Display(Description = "آقا")]
        Male = 1,

        [Display(Description = "خانم")]
        Female = 2,
    }
}
