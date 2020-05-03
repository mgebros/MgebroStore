using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Consultant
{
    public class SearchByFrequentlySoldProductsViewModel
    {
        public List<SearchByFrequentlySoldProductsItem> Items { get; set; } = new List<SearchByFrequentlySoldProductsItem>();

        [DisplayName("ჯამური თანხა")]
        public float TotalQuantity { get; set; }
    }

    public class SearchByFrequentlySoldProductsItem
    {

        [DisplayName("იდენტიფიკატორი")]
        public int ID { get; set; }

        [DisplayName("კონსულტანტი")]
        public string FullName { get; set; }
        
        [DisplayName("პირადობა")]
        public string PID { get; set; }

        [DisplayName("დაბ. თარიღი")]
        public DateTime Birthdate { get; set; }

        [DisplayName("კოდი")]
        public int Code { get; set; }

        [DisplayName("რაოდენობა")]
        public float Quantity { get; set; }
    }
}
