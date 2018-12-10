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
    }
}