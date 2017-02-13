using Abc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.WebUI.Models
{
    public class ProductAddVM
    {
        public List<Category> Categories { get; internal set; }
        public Product Product { get; set; }
    }
}
