using Domain.Anemic.Entities;
using NazmMapping.Mappings;

namespace ViewModels.Controllers
{
    public class ControllerCreateViewModel : IMapFrom<Controller>
    {
        public string TitleFa { get; set; }
        public string TitleEn { get; set; }
    }
}
