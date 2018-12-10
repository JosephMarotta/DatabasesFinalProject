using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
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

            return View(Borrowers.ToList());
        }
    }
}