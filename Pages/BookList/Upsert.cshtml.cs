using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();

            if (id == null)
            {
                //create
                return Page();
            }

            //update
            Book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _dbContext.Books.Add(Book);
                }
                else
                {
                    _dbContext.Books.Update(Book);
                }

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

