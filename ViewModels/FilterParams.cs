
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ViewModels
{
    public class FilterParams
    {
        public List<FilterItem> Filter
        {
            get
            {
                return JsonConvert.DeserializeObject<List<FilterItem>>(FilterJson ?? "") ?? new List<FilterItem>();
            }
        }
        public string SortBy { get; set; }
        public string FilterJson { get; set; }
    }
}
