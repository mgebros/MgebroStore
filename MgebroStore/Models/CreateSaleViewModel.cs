using MgebroStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models
{
    public class CreateSaleViewModel
    {
        [DisplayName("კონსულტანტი")]
        public int SellerID { get; set; }
        public List<int> ProductIDs { get; set; } = new List<int>();
        public List<int> ProductQuantity { get; set; } = new List<int>();

        public string ProductIDsText { get; set; }
        public string ProductQuantityText { get; set; }

        public bool CastVariables()
        {
            try
            {
                ProductIDs.AddRange(ProductIDsText.Split(',').Select(s => int.Parse(s)));
                ProductQuantity.AddRange(ProductQuantityText.Split(',').Select(s => int.Parse(s)));
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

    }


}
