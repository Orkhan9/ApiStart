using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstart.ViewModels
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public ICollection<string> Genre { get; set; }
        public int GenreId { get; set; }
    }
}
