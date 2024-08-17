using Domain.Anemic.Entities;
using NazmMapping.Mappings;

namespace ViewModels.ActionMethods
{
    public class ActionMethodUpdateViewModel : IMapFrom<ActionMethod>
    {
        public int ActionMethodId { get; set; }
        public int ControllerId { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
    }
}
