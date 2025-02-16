using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [EnableQuery]
        [HttpGet] // GET odata/Books
        public IActionResult GetAll()
        {
            return Ok(_bookRepository.GetAll());
        }

        [EnableQuery]
        [HttpGet("get-by-id")]
        public IActionResult GetById([FromODataUri] int key)
        {
            var book = _bookRepository.GetById(key);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost] // POST odata/Books
        public IActionResult Create([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _bookRepository.Add(book);
            return Created($"odata/Books({book.book_id})", book);
        }

        [HttpPut] // PUT odata/Books/{key}
        public IActionResult Update(int key, [FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != book.book_id) return BadRequest();

            _bookRepository.Update(book);
            return NoContent();
        }

        [HttpDelete] // DELETE odata/Books/{key}
        public IActionResult Remove(int key)
        {
            var book = _bookRepository.GetById(key);
            if (book == null) return NotFound();

            _bookRepository.Delete(key);
            return NoContent();
        }
    }
}
