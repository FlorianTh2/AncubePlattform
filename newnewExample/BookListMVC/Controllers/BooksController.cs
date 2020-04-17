using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{
    [Route("[controller]/[action]")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }




        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }



        [Route("~/Books")]
        public IActionResult Index()
        {
            return View();
        }


        // create + update in one method
        public IActionResult Upsert(int? id)
        {
            Book = new Book();
            if(id==null)
            {
                //create
                return View(Book);
            }
            //update
            Book = _db.Books.FirstOrDefault(a => a.Id == id);
            if(Book==null)
            {
                return NotFound();
            }
            return View(Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert([Bind("Id, Name, Author, ISBN")] Book book)
        {
            if(ModelState.IsValid)
            {
                if(book.Id ==0)
                {
                    //create
                    _db.Books.Add(Book);
                }
                else
                {
                    _db.Books.Update(book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }



        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }




        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if(bookFromDb==null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion

    }
}