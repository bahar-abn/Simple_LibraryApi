using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;
using LibraryApi.Models;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;
        public BooksController(LibraryContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            book.Id = Guid.NewGuid(); 
            _context.Books.Add(book);
            _context.SaveChanges();  
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books.ToList();  
            return Ok(books);  
        }
        [HttpGet("{id}")]
        public IActionResult GetBook(Guid id)
        {
            var book = _context.Books.Find(id); 
            if (book == null)
            {
                return NotFound();  
            }
            return Ok(book);  
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(Guid id, Book book)
        {
            if (id != book.Id) return BadRequest("Error");  
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            var book = _context.Books.Find(id);  
            if (book == null)
            {
                return NotFound();  
            }
            _context.Books.Remove(book);  
            _context.SaveChanges();  
            return NoContent(); 
        }
    }
}