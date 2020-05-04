using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Data.Entities
{
    public class Consultant
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ სახელი")]
        [DisplayName("სახელი")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ გვარი")]
        [DisplayName("გვარი")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პირადობა")]
        [DisplayName("პირადობა")]
        [MaxLength(11)]
        public string PID { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ სქესი")]
        [DisplayName("სქესი")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ თარიღი")]
        [DisplayName("დაბ. თარიღი")]
        [DataType(DataType.Date)]
        //[Range(typeof(DateTime), "1.1.1940", "3.4.2002",
        //ErrorMessage = "დასაშვებია {1}-დან  {2}-მდე")]
        public DateTime Birthdate { get; set; }

        [DisplayName("რეკომენდატორი")]
        public int ReferrerID { get; set; }

        [DisplayName("რეკომენდატორი")]
        [NotMapped]
        public string ReferralName { get; set; }



        public string GetFullName()
        {
            return LastName + " " + FirstName;
        }

    }


    public enum Gender
    {
        [Display(Name = "მამაკაცი")]
        Male = 0,

        [Display(Name = "ქალი")]
        Female = 1
    }

}
