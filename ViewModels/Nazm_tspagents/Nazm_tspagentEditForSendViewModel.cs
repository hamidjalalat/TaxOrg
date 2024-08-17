using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;


namespace ViewModels.Nazm_tspagents
{
    public class Nazm_tspagentEditForSendViewModel : IMapFrom<Nazm_tspagent>
    {
        public int id { get; set; }
        public decimal fee { get; set; }
    }
}
