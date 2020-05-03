using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Sale
{
    public class SearchByPricesViewModel
    {
        public List<SearchByPricesItem> Items { get; set; } = new List<SearchByPricesItem>();

        [DisplayName("ჯამური თანხა")]
        public float TotalPrice { get; set; }
    }

    public class SearchByPricesItem
    {
        [DisplayName("იდენტიფიკატორი")]
        public int ID { get; set; }

        [DisplayName("თარიღი")]
        public DateTime SaleDate { get; set; }

        [DisplayName("კონსულტანტი")]
        public string FullName { get; set; }

        [DisplayName("პირადობა")]
        public string PID { get; set; }

        [DisplayName("თანხა")]
        public float TotalPrice { get; set; }
    }

}
