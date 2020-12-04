using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstart.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<PatientDoctor> PatientDoctors { get; set; }
    }
}
