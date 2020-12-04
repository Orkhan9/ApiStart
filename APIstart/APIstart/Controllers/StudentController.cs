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
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public StudentController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public ActionResult<IEnumerable <Student>> Get()
        {
            var students = _db.Students.Include(s => s.StudentCourses).ThenInclude(k => k.Course).ToList();
            var mapperStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students);
            return Ok(mapperStudents);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = _db.Students.Include(s => s.StudentCourses).ThenInclude(k => k.Course).FirstOrDefault(s=>s.Id==id);
            if (student == null) return NotFound();
            var mapperStudent = _mapper.Map<StudentDto>(student);
            return Ok(mapperStudent);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
