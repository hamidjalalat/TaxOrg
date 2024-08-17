using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class EmailConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}
