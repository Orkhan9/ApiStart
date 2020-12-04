using APIstart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstart.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public ICollection<GenreBook> GenreBooks { get; set; }
        public Genre Genre { get; set; }
        public Book Book { get; set; }
    }
}
