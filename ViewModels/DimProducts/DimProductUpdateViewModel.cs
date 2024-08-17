using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DimProducts
{
    public class DimProductUpdateViewModel:IMapFrom<DimProduct>
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
