using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Entities.Concrete;

namespace Abc.WebUI.Models
{
    public class CategoryListVM
    {
        public List<Category> Categories { get; internal set; }
        public int CurrentCategory { get; internal set; }
    }
}
