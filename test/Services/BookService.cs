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

            //string conString = "User Id=myers;Password=006124432;Data Source=dataserv.mscs.mu.edu:1521/orcl";
            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
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

        public Book GetBook(int BookId)
        {
            Book book = new Book();

            //string conString = "User Id=myers;Password=006124432;Data Source=dataserv.mscs.mu.edu:1521/orcl";
            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = $"SELECT * FROM BOOK WHERE BOOKID = '{BookId}'";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    book = new Book(
                     reader.GetInt32(0),
                     reader.GetString(1),
                     reader.GetString(2)
                    );
                }
            }
            else
            {
            }

            reader.Close();
            conn.Close();
            conn.Dispose();

            return book;
        }

        public List<BookLoan> GetBorrowerBooks(string cardNo)
        {
            List<BookLoan> BookLoans = new List<BookLoan>();

            //string conString = "User Id=myers;Password=006124432;Data Source=dataserv.mscs.mu.edu:1521/orcl";
            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = $"SELECT B.Name, K.Title, L.dateout, L.duedate, L.datein" +
                            $" FROM BORROWER B, BOOK_LOANS L, BOOK K" +
                            $" WHERE B.CardNo = '{cardNo}' AND B.CardNo = L.CardNo AND K.bookid = L.bookid" +
                            $" GROUP BY B.CardNo, B.Name, K.Title, l.dateout, L.duedate, L.datein";

            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BookLoans.Add(new BookLoan(
                     reader.GetString(0),
                     reader.GetString(1),
                     reader.GetDateTime(2),
                     reader.GetDateTime(3)
                    ));
                }
            }
            else
            {
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            return BookLoans;
        }

        // Limitations : Does not account for Branch of library
        public Book CheckoutBook(string CardNo, int BookId)
        {
            Book ret = new Book();

            //string conString = "User Id=myers;Password=006124432;Data Source=dataserv.mscs.mu.edu:1521/orcl";
            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = $"INSERT INTO BOOK_LOANS VALUES ('{BookId}','7067','{CardNo}',sysdate,add_months(sysdate,1),null, 8)";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Close();

            sqlCmd = $"SELECT * FROM BOOK WHERE BOOKID = '{BookId}'";

            cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ret = new Book(
                     reader.GetInt32(0),
                     reader.GetString(1),
                     reader.GetString(2)
                    );
                }
            }
            else
            {
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            return ret;
        }
    }
}