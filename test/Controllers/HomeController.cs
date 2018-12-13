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

            List<BookLoan> BookLoans = _BookService.GetBorrowerBooks(CardNo);

            return View(BookLoans.ToList());
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

        // GET: Home
        public ActionResult Success(string CardNo)
        {
            ViewBag.CurrentUser = _BorrowerService.GetBorrower(CardNo);

            return View();
        }

        // GET: Home
        public ActionResult Book(string CardNo)
        {
            ViewBag.CurrentUser = _BorrowerService.GetBorrower(CardNo);
            List<BookLoan> BookLoans = _BookService.GetBorrowerBooks(CardNo);
            List<Book> Books = _BookService.GetBooks();

            foreach(BookLoan loan in BookLoans)
            {
                Books.Remove(Books.Single(s => s.Title.Equals(loan.Title)));
            }
            
            return View(Books.ToList());
        }

        public ActionResult CheckoutBook(string CardNo, int BookId)
        {
            Book check = _BookService.CheckoutBook(CardNo, BookId);

            return RedirectToAction("/CheckoutSuccess/", new { CardNo = CardNo, BookId = check.Id });
        }

        // GET: Home
        public ActionResult CheckoutSuccess(string CardNo, int BookId)
        {
            ViewBag.CurrentUser = _BorrowerService.GetBorrower(CardNo);
            ViewBag.DueDate = DateTime.Now.AddMonths(1);
            Book book = _BookService.GetBook(BookId);

            return View(book);
        }
    }
}