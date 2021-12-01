using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        
        [BindProperty]
        public Book Book { get; set; }

        public EditModel(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task OnGet(int id)
        {
            Book = await _dbContext.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookInDb = await _dbContext.Books.FindAsync(Book.Id);
                bookInDb.Name = Book.Name;
                bookInDb.Author = Book.Author;
                bookInDb.ISBN = Book.ISBN;

                await _dbContext.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
