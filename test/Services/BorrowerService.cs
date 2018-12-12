using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class BorrowerService
    {
        // Method for retrieving all Borrowers from the database
        public List<Borrower> GetBorrowers()
        {
            List<Borrower> Borrowers = new List<Borrower>();

            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = "select * from BORROWER";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Borrowers.Add(new Borrower(
                     reader.GetString(0),
                     reader.GetString(1),
                     reader.GetString(2),
                     reader.GetString(3),
                     reader.GetString(4)
                    ));
                }
            }
            else
            {
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            return Borrowers;
        }

        public Borrower GetBorrower(string cardNo)
        {
            Borrower borrower = new Borrower();

            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = $"SELECT * FROM BORROWER WHERE CARDNO = '{cardNo}'";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    borrower = new Borrower(
                     reader.GetString(0),
                     reader.GetString(1),
                     reader.GetString(2),
                     reader.GetString(3),
                     reader.GetString(4)
                    );
                }
            }
            else
            {
                // Borrower not in database
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            return borrower; 
        }

        // Method for checking if a borrower is in the database
        // used for logging in a borrower
        public bool TryLogin(string cardNo, string password)
        {
            Borrower borrower = new Borrower(); 

            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = $"SELECT * FROM BORROWER WHERE CARDNO = '{cardNo}'";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    borrower = new Borrower(
                     reader.GetString(0),
                     reader.GetString(1),
                     reader.GetString(2),
                     reader.GetString(3),
                     reader.GetString(4)
                    );
                }
            }
            else
            {
                // Borrower not in database
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            if (password.Equals(borrower.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Borrower RegisterBorrower(string name, string address, string phoneNo, string password)
        {
            Borrower borrower = new Borrower();

            string conString = "User Id=system;Password=furgpet1;Data Source=LAPTOP-JOE";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string sqlCmd = $"INSERT INTO BORROWER VALUES(borrower_seq.nextval, '{name}', '{address}', '{phoneNo}', '{password}')";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Close();
            sqlCmd = $" SELECT * FROM BORROWER WHERE NAME = '{name}' AND ADDRESS = '{address}' AND PHONE = '{phoneNo}'";
            cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    borrower = new Borrower(
                     reader.GetString(0),
                     reader.GetString(1),
                     reader.GetString(2),
                     reader.GetString(3),
                     reader.GetString(4)
                    );
                }
            }
            else
            {
                // Borrower failed to be created
            }
            reader.Close();
            conn.Close();
            conn.Dispose();

            return borrower;
        }
    }
}