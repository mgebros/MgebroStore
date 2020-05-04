using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Sale
{
    public class SearchByPricesRequest : IPagination
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public float FromPrice { get; set; }
        public float ToPrice { get; set; }
    }
}
