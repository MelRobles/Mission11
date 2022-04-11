using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission7_Books.Infrastructure;
using Mission7_Books.Models;

namespace Mission7_Books.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private IBookProjectRepository repo { get; set; }
        public ShoppingCartModel (IBookProjectRepository temp, Basket b)
        {
            repo = temp;
            basket = b; //noramlly want to do this beforehand
        }

        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            //above is the original session code. Leaving in for notes purposes.
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            //above is the original session code, I'm leaving it for notes
            
            basket.AddItem(b, 1);

            //HttpContext.Session.SetJson("basket", basket);
            //same as above comment

            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove (int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);

            return RedirectToPage ( new {ReturnUrl = returnUrl});
        }
    }
}
