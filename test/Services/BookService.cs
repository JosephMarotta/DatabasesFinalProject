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

            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = "select * from BOOK";
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
    }
}