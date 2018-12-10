using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string conString = "User Id=system;Password=furgpet1;Data Source=DESKTOP-NGUAH05";
            OracleConnection conn = new OracleConnection(conString);
            conn.Open();
            string temp = ""; 
            string sqlCmd = "select * from BORROWER";
            OracleCommand cmd = new OracleCommand(sqlCmd, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    temp = reader.GetString(0);
                    temp = reader.GetString(1);
                    temp = reader.GetString(2);
                    temp = reader.GetString(3);
                    temp = reader.GetString(4);
                }
            }
            else
            {
            }
            reader.Close();


            conn.Close();
            conn.Dispose();
            return View();
        }
    }
}