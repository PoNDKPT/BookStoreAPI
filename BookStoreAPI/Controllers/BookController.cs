using BookStoreAPI.Models;
using BookStoreAPI.Services.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class BookController : ControllerBase
    {
        private readonly IBookService BookService;

        public BookController(IBookService BookService)
        {
            this.BookService = BookService;
        }
        // GET: api/<BookController>
        [HttpGet]
        public async Task<List<BookModel>> Get()
        {
            return await BookService.Get();
        }

        // GET api/<BookController>/id
        [HttpGet("Find/{id}")]
        public async Task<ActionResult<BookModel>> Find(string id)
        {
            var book = await BookService.FindId(id);
            if(book == null)
            {
                return NotFound($"Book: Id = {id} not found");
            }
            return book;
        }

        // POST api/<BookController>
        [HttpPost("Post")]
        public async Task<ActionResult<BookModel>> Post([FromBody] BookFormModel book)
        {
            BookModel bookModel = new BookModel()
            {
                Title = book.Title,
                Price = book.Price,
                Category = book.Category,
                Actors = book.Actors
            };
            await BookService.Create(bookModel);
            return CreatedAtAction(nameof(Get), book);
        }

        // PUT api/<BookController>/5
        [HttpPut("Put/{id}")]
        public async Task<ActionResult<BookModel>> Put(string id, [FromBody] BookModel UpdateBook)
        {
            var book = await BookService.FindId(id);
            if (book == null)
            {
                return NotFound($"Book: Id = {id} not found");
            }
            UpdateBook.Id = book.Id;
            await BookService.Update(id, UpdateBook);
            return await BookService.FindId(id);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var book = await BookService.FindId(id);
            if (book is null)
            {
                return NotFound($"Book: Id = {id} not found");
            }
            await BookService.Delete(book.Id);
            return NoContent();
        }
    }
}
