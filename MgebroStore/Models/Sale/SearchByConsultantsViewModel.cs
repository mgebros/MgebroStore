using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Sale
{
    public class SearchByConsultantsViewModel
    {
        public List<SearchByConsultantsItem> Items { get; set; } = new List<SearchByConsultantsItem>();

        [DisplayName("ჯამური თანხა")]
        public float TotalPrice { get; set; }

    }

    public class SearchByConsultantsItem
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
