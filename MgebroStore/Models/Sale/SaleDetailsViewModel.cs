using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Sale
{
    public class SaleDetailsViewModel
    {
        [DisplayName("პირადობა")]
        public string PID { get; set; }

        [DisplayName("კონსულტანტი")]
        public string Seller { get; set; }

        [DisplayName("გაყიდვის თარიღი")]
        public DateTime SaleDate { get; set; }

        [DisplayName("ჯამური თანხა")]
        public float TotalPrice { get; set; }

        public List<SaleItem> SoldItems { get; set; } = new List<SaleItem>();

    }
}
