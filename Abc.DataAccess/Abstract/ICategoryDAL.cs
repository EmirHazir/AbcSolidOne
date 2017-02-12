using Abc.Core.Repository;
using Abc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.DataAccess.Abstract
{
   public interface ICategoryDAL:IEntityRepository<Category>
    {
        //Custom Oprations will be here if you need like stored procedures etc
    }
}
