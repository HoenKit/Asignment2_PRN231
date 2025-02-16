using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    [Route("odata/[controller]")]
    public class PublishersController : ODataController
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAll()
        {
            return Ok(_publisherRepository.GetAll());
        }
        [HttpGet("get-by-id")]
        [EnableQuery]
        public IActionResult GetById([FromODataUri] int key)
        {
            var publisher = _publisherRepository.GetById(key);
            if (publisher == null) return NotFound();
            return Ok(publisher);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _publisherRepository.Add(publisher);
            return Created($"odata/Publishers({publisher.pub_id})", publisher);
        }
        [HttpPut]
        public IActionResult Update([FromODataUri] int key, [FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != publisher.pub_id) return BadRequest();

            _publisherRepository.Update(publisher);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Remove([FromODataUri] int key)
        {
            var publisher = _publisherRepository.GetById(key);
            if (publisher == null) return NotFound();

            _publisherRepository.Delete(key);
            return NoContent();
        }
    }
}
