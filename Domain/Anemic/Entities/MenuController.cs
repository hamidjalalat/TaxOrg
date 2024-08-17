using Domain.Anemic.Common;
using EntityFrameworkCore.AutoHistory.Attributes;
using System.Collections.Generic;

namespace Domain.Anemic.Entities
{
    [ExcludeFromHistory]
    public class MenuController : Entity
    {
        public int MenuControllerId { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public int ControllerId { get; set; }
        public Controller? Controller { get; set; }


    }
}
