using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShopPodaci.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        
        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }


        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("Gender")]
        public int GenderID { get; set; }
        public Gender Gender { get; set; }

        [ForeignKey("CreditCard")]
        public int? CreditCardID { get; set; }
        public CreditCard? CreditCard { get; set; }
    }
}
