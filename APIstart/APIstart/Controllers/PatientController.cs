using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIstart.DAL;
using APIstart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIstart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PatientController(AppDbContext db)
        {
            _db = db;
        }
        // GET: api/<PatientController>
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {
            return Ok(_db.Patients.Include(p=>p.PatientDoctors).ThenInclude(d=>d.Doctor));
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public ActionResult<Patient> Get(int id)
        {
            return Ok(_db.Patients.Include(p => p.PatientDoctors).ThenInclude(d => d.Doctor).FirstOrDefault(p=>p.Id==id));
        }

        // POST api/<PatientController>
        [HttpPost]
        public async Task<ActionResult<Patient>> Post([FromBody] Patient patient)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> Put(int id, [FromBody] Patient patient)
        {
            if (patient.Id != id) return BadRequest();
            Patient dbPatient = _db.Patients.FirstOrDefault(p => p.Id == patient.Id);
            dbPatient.Name = patient.Name;
            dbPatient.Age = patient.Age;

            List<PatientDoctor> patientDoctors = _db.PatientDoctors.Where(p => p.PatientId == patient.Id).ToList();
            foreach (var item in patientDoctors)
            {
                dbPatient.PatientDoctors.Remove(item);
            }

            List<PatientDoctor> newPatientDoctors = new List<PatientDoctor>();
            foreach (var item in patient.PatientDoctors)
            {
                PatientDoctor patientDoctor = new PatientDoctor
                {
                    PatientId = dbPatient.Id,
                    DoctorId = item.DoctorId
                };
                newPatientDoctors.Add(patientDoctor);
            }
            dbPatient.PatientDoctors = newPatientDoctors;
            await _db.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
