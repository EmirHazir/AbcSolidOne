using Abc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Entities.Concrete;
using Abc.DataAccess.Abstract;

namespace Abc.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDAL _categoryDal;
        public CategoryManager(ICategoryDAL categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }
    }
}
