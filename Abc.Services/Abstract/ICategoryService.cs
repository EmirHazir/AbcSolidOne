using Abc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Services.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}
