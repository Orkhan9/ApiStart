using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIstart.DAL;
using APIstart.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIstart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PostController(AppDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Get All Posts
        /// </summary>
        /// <returns></returns>
        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _db.Posts;
        }

        /// <summary>
        /// Get Post from Id
        /// </summary>
        /// <param name="id">for post</param>
        /// <returns></returns>
        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            Post post = _db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            return post;
        }

        /// <summary>
        /// Create new Post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        // POST api/<PostController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Post post)
        {
            if (!ModelState.IsValid) return BadRequest();
            Post dbpost = new Post
            {
                Title = post.Title,
                Content = post.Content
            };
            await _db.AddAsync(dbpost);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Update Post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody] Post post)
        {
            if (id != post.Id) return BadRequest();
            Post dbpost = _db.Posts.FirstOrDefault(p => p.Id == id);
            if (dbpost == null) return NotFound();

            dbpost.Title = post.Title;
            dbpost.Content = post.Content;
            await _db.SaveChangesAsync();
            return dbpost;
        }

        /// <summary>
        /// Delete Post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Post dbpost = _db.Posts.FirstOrDefault(p => p.Id == id);
            if (dbpost == null) return NotFound();
            _db.Posts.Remove(dbpost);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
