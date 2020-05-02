using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Data.Entities
{
    public class Sale
    {
        [DisplayName("იდენტიფიკატორი")]
        public int ID { get; set; }
        [DisplayName("თარიღი")]
        public DateTime SaleDate { get; set; }
        public string SellerInfo { get; set; }
        public string Description { get; set; }
        [DisplayName("ჯამური თანხა")]
        public float TotalPrice { get; set; }
    }

}
