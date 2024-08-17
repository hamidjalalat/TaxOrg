using Domain.Anemic.Entities;
using NazmMapping.Mappings;

namespace ViewModels.ActionMethods
{
    public class ActionMethodCreateViewModel : IMapFrom<ActionMethod>
    {
        public int ControllerId { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
    }
}
