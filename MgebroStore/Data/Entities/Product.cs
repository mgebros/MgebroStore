using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Data.Entities
{
    public class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ კოდი")]
        [Range(1000, 999999, ErrorMessage = "სიგრძე დასაშვებია 4-დან 6 ციფრამდე")]
        [DisplayName("კოდი")]
        public int Code { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ დასახელება")]
        [DisplayName("დასახელება")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ ფასი")]
        [DisplayName("ფასი")]
        public float Price { get; set; }

    }
}
