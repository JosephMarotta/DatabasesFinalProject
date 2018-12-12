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
        public ActionResult Index(string CardNo)
        {
            ViewBag.CurrentUser = _BorrowerService.GetBorrower(CardNo);
            List<Book> Books = _BookService.GetBooks();

            return View();
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
                return RedirectToAction("/Index/", new { CardNo = cardNo });
            else
                return RedirectToAction("/Login", new { message = "Incorrect Card Number or Password. Please try again" });
        }

        // GET: Home
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterBorrower(string name, string address, string phoneNo, string password)
        {
            Borrower test = _BorrowerService.RegisterBorrower(name, address, phoneNo, password);

            return RedirectToAction("/Success/", new { CardNo = test.CardNo });
        }

        public ActionResult Success(string CardNo)
        {
            ViewBag.CurrentUser = _BorrowerService.GetBorrower(CardNo);

            return View();
        }

        public ActionResult Book(string CardNo)
        {
            ViewBag.CurrentUser = _BorrowerService.GetBorrower(CardNo);
            List<Book> Books = _BookService.GetBooks();

            return View(Books.ToList());
        }

    }
}