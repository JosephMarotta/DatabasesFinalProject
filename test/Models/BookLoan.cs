using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class BookLoan
    {
        public string BorrowerName { get; set; }
        public string Title { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DueDate { get; set; }

        public BookLoan(){ }

        public BookLoan(string bName, string title, DateTime dOut, DateTime dueD)
        {
            BorrowerName = bName;
            Title = title;
            DateOut = dOut;
            DueDate = dueD;
        }
    }
}