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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetList()
        {
            var products = _db.Products.Include(x => x.Category);
            var mapperproducts = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products);
            return Ok(mapperproducts);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = _db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            var mapperproduct = _mapper.Map<ProductDto>(product);
            return Ok(mapperproduct);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductUpdateVM productUpdateVM)
        {
            if (id != productUpdateVM.Id) return BadRequest();
            var dbproduct = _db.Products.Include(x=>x.Category).FirstOrDefault(p => p.Id == id);
            if (dbproduct == null) return NotFound();

            dbproduct.Name = productUpdateVM.Name;
            dbproduct.Description = productUpdateVM.Description;
            dbproduct.CategoryId = productUpdateVM.CategoryId;
            await _db.SaveChangesAsync();
            return Ok();
        }

        

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Product dbproduct = _db.Products.FirstOrDefault(p => p.Id == id);

            if (dbproduct == null) return NotFound();
            _db.Products.Remove(dbproduct);
            await _db.SaveChangesAsync();
            return Ok();
        }
        
        [HttpGet("categories")]
        public async Task<ActionResult> GetCategories()
        {
            return Ok(await _db.Categories.ToListAsync());
        }
    }
}
