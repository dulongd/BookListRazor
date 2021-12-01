﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new {data = await _dbContext.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookInDb == null)
            {
                return Json(new {success = false, message = "Error while Deleting"});
            }

            _dbContext.Books.Remove(bookInDb);
            await _dbContext.SaveChangesAsync();

            return Json(new {success = true, message = "Delete successful"});

        }
    }
}
