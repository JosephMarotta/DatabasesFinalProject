using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class Borrower
    {
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }

        public Borrower(){ }

        public Borrower(string cardNo, string name, string address, string phone, string password)
        {
            CardNo = cardNo;
            Name = name;
            Address = address;
            PhoneNo = phone;
            Password = password;
        }
    }
}