using System.Collections.Generic;
using Abc.Entities.Concrete;

namespace Abc.WebUI.Models
{
    public class ProductUpdateVM
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }
    }
}