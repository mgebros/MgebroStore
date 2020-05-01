﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Entities
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
        [DisplayName("დაბადების თარიღი")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [DisplayName("რეკომენდატორი")]
        public int Referral { get; set; } = 0;
    }


    public enum Gender
    {
        [Display(Name = "მამაკაცი")]
        Male = 0,

        [Display(Name = "ქალი")]
        Female = 1
    }

}
