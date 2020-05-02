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
        public string SalerInfo { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public Consultant Saler { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }

    }
}
