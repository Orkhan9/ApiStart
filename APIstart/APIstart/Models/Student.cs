using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstart.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
