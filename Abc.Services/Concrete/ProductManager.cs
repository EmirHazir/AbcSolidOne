using Abc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Entities.Concrete;
using Abc.DataAccess.Abstract;

namespace Abc.Services.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDAL _productDal;

        public ProductManager(IProductDAL productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(int productId)
        {
            _productDal.Delete(new Product { ProductId = productId });
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            //categoryId == 0 a uyan bişey yok bunu yapmamızın nedeni sayfalama yaparken herhangi bir categori belirtmediğimde tüm ürünlerin gelmesini sağlamak
            return _productDal.GetList(p => p.CategoryId == categoryId ||categoryId== 0);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public Product GetById(int productId)
        {
          return _productDal.Get(p => p.ProductId == productId);
        }
    }
}
