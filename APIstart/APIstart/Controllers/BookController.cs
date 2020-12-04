using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIstart.DAL;
using APIstart.Models;
using APIstart.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIstart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public BookController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // GET: api/<BookController>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            //var books = _db.Books.Include(b => b.GenreBooks).ThenInclude(b => b.Genre)
            //    .Select(b => new
            //    {
            //        Id = b.Id,
            //        Name = b.Name,
            //        Author = b.Author,
            //        GenreBooks = b.GenreBooks
            //    .Select(gb => new { 
            //        Id = gb.Id,
            //        Genre = gb.Genre
            //    })
            //    }).ToList();
            var books = _db.Books.Include(b => b.GenreBooks).ThenInclude(b => b.Genre);
            var mapBooks = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);

            return Ok(mapBooks);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            Book dbbook = _db.Books.Include(b=>b.GenreBooks).ThenInclude(b=>b.Genre).FirstOrDefault(b => b.Id == id);
            if (dbbook == null) return NotFound();

            var mapbook = _mapper.Map<BookDto>(dbbook);

            //var book = _db.Books.Include(b => b.GenreBooks).ThenInclude(b => b.Genre)
            //    .Select(b => new
            //    {
            //        Id = b.Id,
            //        Name = b.Name,
            //        Author = b.Author,
            //        GenreBooks = b.GenreBooks
            //    .Select(gb => new {
            //        Id = gb.Id,
            //        Genre = gb.Genre
            //    })
            //    }).FirstOrDefault(b=>b.Id==id);
            
            return Ok(mapbook);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return Ok(book);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest();
            var dbbook = _db.Books.Include(b=>b.GenreBooks).FirstOrDefault(b => b.Id == id);
            if (dbbook == null) return NotFound();

            dbbook.Name = book.Name;
            dbbook.Author = book.Author;

            var dbgenrebooks = _db.GenreBooks.Where(b => b.BookId == dbbook.Id).ToList();
            foreach (var item in dbgenrebooks)
            {
                dbbook.GenreBooks.Remove(item);
            }

            var genreBooks = new List<GenreBook>();
            foreach (var item in book.GenreBooks)
            {
                GenreBook genreBook = new GenreBook
                {
                    GenreId = item.GenreId,
                    BookId = dbbook.Id
                };
                genreBooks.Add(genreBook);
            }
            dbbook.GenreBooks = genreBooks;
            await _db.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Book dbbook = _db.Books.FirstOrDefault(b => b.Id == id);

            var dbgenrebooks = _db.GenreBooks.Where(b => b.BookId == dbbook.Id).ToList();
            foreach (var item in dbgenrebooks)
            {
                dbbook.GenreBooks.Remove(item);
            }

            _db.Books.Remove(dbbook);
            await _db.SaveChangesAsync();
            return Ok();
        }

        
    }
}
