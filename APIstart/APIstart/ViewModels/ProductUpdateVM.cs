using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstart.ViewModels
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
