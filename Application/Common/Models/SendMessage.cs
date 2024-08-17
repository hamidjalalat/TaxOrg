using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class SendMessage
    {
        public string To { get; set; }
        public string Message { get; set; }
        public string? Title { get; set; }
    }
}
