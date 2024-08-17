using Domain.Anemic.Entities;
using NazmMapping.Mappings;

namespace ViewModels.Controllers
{
    public class ControllerUpdateViewModel : IMapFrom<Controller>
    {
        public int ControllerId { get; set; }
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
    }
}
