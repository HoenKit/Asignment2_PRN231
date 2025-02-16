using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    [ODataRouteComponent("odata")]
    [Route("odata/[controller]")]
    [ApiController]
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
        [HttpPost]
        public IActionResult Create([FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _authorRepository.Add(author);
            // Use the standard Created method
            return Created($"odata/Authors({author.author_id})", author);
        }

        // PUT: odata/Authors(1)
        [HttpPut]
        public IActionResult Update( int key, [FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != author.author_id) return BadRequest();

            _authorRepository.Update(author);
            return NoContent();
        }

        // DELETE: odata/Authors(1)
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
