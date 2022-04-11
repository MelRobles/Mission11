using Microsoft.AspNetCore.Mvc;
using Mission7_Books.Models;
using Mission7_Books.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission7_Books.Controllers
{
    public class HomeController : Controller
    {

        private IBookProjectRepository repo;

        //I'm keeping the code below for notes

        //private BookstoreContext context { get; set; }

        //public HomeController(BookstoreContext temp) => context => temp;

        public HomeController (IBookProjectRepository temp)
        {
            repo = temp;
        }
        public IActionResult Index(string bookCategory, int pageNum = 1) //one here is the default
        {
            int pageSize = 5;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == bookCategory || bookCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks =
                        (bookCategory == null
                            ? repo.Books.Count()
                            : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        //public IActionResult Index() => View();
        //These are the same thing.I'm leaving this code for notes purposes
    }
}
