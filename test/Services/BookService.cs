using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class BookService
    {
        public List<Book> GetBooks()
        {
            List<Book> Books = new List<Book>();

            string conString = "User Id=myers;Password=006124432;Data Source=dataserv.mscs.mu.edu:1521/orcl";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = "SELECT * FROM BOOK";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Books.Add(new Book(
                     reader.GetInt32(0),
                     reader.GetString(1),
                     reader.GetString(2)
                    ));
                }
            }
            else
            {
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            return Books;
        }

        public void Checkout(string BookId, string CardNo)
        {
            string conString = "User Id=myers;Password=006124432;Data Source=dataserv.mscs.mu.edu:1521/orcl";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlcmd = "select b.BookId, b.Title from Book b, Book_copies c where b.BookId = c.BookId";
        }
    }
}