using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Data.Entities
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime SaleDate { get; set; }
        public string SellerInfo { get; set; }
        public string Description { get; set; }
        public float TotalPrice { get; set; }


    }



    [NotMapped]
    public class SaleItem
    {
        public int Code { get; set; }   
        public string Title { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
