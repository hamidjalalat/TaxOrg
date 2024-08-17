using Domain.Anemic.Entities;

namespace ViewModels.MenuControllers
{
    public class MenuControllerCreateViewModel
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int ControllerId { get; set; }
    }
}
