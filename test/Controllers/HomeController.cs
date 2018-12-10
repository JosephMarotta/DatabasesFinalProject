using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using test.Models;
using test.Services;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private BorrowerService _BorrowerService;
        private BookService _BookService;

        public HomeController()
        {
            _BorrowerService = new BorrowerService();
            _BookService = new BookService();
        }

        // GET: Home
        public ActionResult Index()
        {
            List<Book> Books = _BookService.GetBooks(); 

            return View(Books.ToList());
        }

        // GET: Home
        [Route("Home/Login/{message?}")]
        public ActionResult Login(string message = null)
        {
            ViewBag.Message = message; 

            return View();
        }

        [HttpPost]
        public ActionResult LoginBorrower(string cardNo, string password)
        {
            bool Success = _BorrowerService.TryLogin(cardNo, password);

            if (Success)
            {
                return RedirectToAction("/Index");
            }
            else
            {
                return RedirectToAction("/Login", new { message = "Error Logging In User." });
            }
        }
    }
}