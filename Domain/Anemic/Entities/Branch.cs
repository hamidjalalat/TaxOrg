using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Anemic.Common;

namespace Domain.Anemic.Entities
{
    public class Branch: Entity
    {
        public int Id { get; set; }
        //public int Nazm_tspagentId { get; set; }
        //public Nazm_tspagent Nazm_tspagent { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
