using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Shared;

namespace ViewModels.Products
{
    public class ProductInputParamsViewModel:PublicViewModel
    {
        public int P_ACNT_NO { get; set; }
        public int P_TR_DT { get; set; }
        public int P_STS { get; set; }
    }
}
