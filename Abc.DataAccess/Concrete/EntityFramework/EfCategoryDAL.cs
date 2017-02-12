using Abc.Core.Repository.EntityFramework;
using Abc.DataAccess.Abstract;
using Abc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDAL : EfRepoBase<Category, NorthwindDbContext>, ICategoryDAL
    {

    }
}
