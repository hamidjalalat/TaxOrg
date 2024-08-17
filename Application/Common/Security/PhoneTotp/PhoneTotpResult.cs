using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Security.PhoneTotp
{
    public class PhoneTotpResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
