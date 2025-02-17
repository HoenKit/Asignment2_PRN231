using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ODataController
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: odata/Authors
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAll()
        {
            return Ok(_authorRepository.GetAll());
        }

        [EnableQuery]
        [HttpGet("get-by-id")] // GET /odata/Authors(1)
        public IActionResult GetById([FromODataUri] int key)
        {
            var author = _authorRepository.GetById(key);
            if (author == null) return NotFound();
            return Ok(author);
        }

        // POST: odata/Authors
        [Authorize(Roles = "Administration")]
        [HttpPost]
        public IActionResult Create([FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _authorRepository.Add(author);
            // Use the standard Created method
            return Created($"odata/Authors({author.author_id})", author);
        }

        // PUT: odata/Authors(1)
        [Authorize(Roles = "Administration")]
        [HttpPut]
        public IActionResult Update( int key, [FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != author.author_id) return BadRequest();

            _authorRepository.Update(author);
            return NoContent();
        }

        // DELETE: odata/Authors(1)
        [Authorize(Roles = "Administration")]
        [HttpDelete]
        public IActionResult Remove( int key)
        {
            var author = _authorRepository.GetById(key);
            if (author == null) return NotFound();

            _authorRepository.Delete(key);
            return NoContent();
        }
    }
}
