using Domain.Anemic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Anemic.Entities
{
    public class Product: BaseEntity
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
