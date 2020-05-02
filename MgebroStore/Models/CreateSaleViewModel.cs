using MgebroStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models
{
    public class CreateSaleViewModel
    {
        public int ConsultantID { get; set; }
        public Consultant Saler { get; set; }

        public List<Product> Products { get; set; }
    }
}
