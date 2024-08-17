using Domain.Anemic.Common;
using EntityFrameworkCore.AutoHistory.Attributes;

namespace Domain.Anemic.Entities
{
    [ExcludeFromHistory]
    public class MenuControllerAction : Entity
    {
        public int MenuControllerActionId { get; set; }
        public int MenuControllerId { get; set; }
        public MenuController? MenuController { get; set; }
        public int ActionMethodId { get; set; }
        public ActionMethod? ActionMethod { get; set; }
    }
}
