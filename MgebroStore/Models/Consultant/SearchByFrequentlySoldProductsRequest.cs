using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Consultant
{
    public class SearchByFrequentlySoldProductsRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Code { get; set; }
        public float MinQuantity { get; set; }
    }
}
