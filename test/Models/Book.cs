using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }

        public Book(int id, string title, string publisher)
        {
            Id = id;
            Title = title;
            Publisher = publisher; 
        }
    }
}