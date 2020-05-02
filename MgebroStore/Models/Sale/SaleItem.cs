using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Sale
{
    public class SaleItem
    {
        [DisplayName("კოდი")]
        public int Code { get; set; }

        [DisplayName("დასახელება")]
        public string Title { get; set; }

        [DisplayName("ფასი")]
        public float Price { get; set; }

        [DisplayName("რაოდენობა")]
        public int Quantity { get; set; }
    }
}
