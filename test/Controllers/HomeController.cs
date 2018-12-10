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
        public ActionResult Login()
        {
            List<Borrower> Borrowers = _BorrowerService.GetBorrowers();

            return View();
        }
    }
}