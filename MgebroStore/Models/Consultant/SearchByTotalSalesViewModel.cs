using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Consultant
{
    public class SearchByTotalSalesViewModel
    {
        public List<SearchByTotalSalesItem> Items { get; set; } = new List<SearchByTotalSalesItem>();

        [DisplayName("ჯამური თანხა")]
        public float TotalSales { get; set; }
        public float HierarchicalTotalSales { get; set; }
    }

    public class SearchByTotalSalesItem
    {

        [DisplayName("იდენტიფიკატორი")]
        public int ID { get; set; }

        [DisplayName("კონსულტანტი")]
        public string FullName { get; set; }

        [DisplayName("პირადობა")]
        public string PID { get; set; }

        [DisplayName("დაბ. თარიღი")]
        public DateTime Birthdate { get; set; }

        [DisplayName("საკუთარი გაყიდვები")]
        public float TotalSales { get; set; }

        [DisplayName("სრული გაყიდვები")]
        public float HierarchicalTotalSales { get; set; }
    }
}
