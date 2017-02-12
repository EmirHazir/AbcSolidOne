using Abc.Services.Abstract;
using Abc.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page = 1,int category = 0)
        {
            int pageSize = 10; //sayfa başına 10 ürün

            //http://localhost:51328/product/index?sayfa=1&kategori=4 böyle bir ifadenin sayfalamadaki küçük bir hacki aslında bu kategori = 0 . productManager daki GetByCategory i incele
            var products = _productService.GetByCategory(category);

            ProductListVM model = new ProductListVM()
            {
                //burada diyelimki linkte sayfa sayısı 3 geldi skip ile gelen sayfa sayısından önceki 10 veriyi çıkarıp sayfaBası ile çarpıyorum ve Take ile kalan sayfaBasi 10 veriyi listeliyorum 
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                //sayfalamada taghelper yazmak için birkac veri girmemiz gerek ilk olarak sayfalamada oluşacak linklerin sayisi.Burada kaç ürün varsa pageSize ya bölerek ve double verip yukarı yuvarlaması için Math.Celling ardından ise tüm yapıyı int e cast ettim 
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page

            };
            return View(model);
        }

       
    }
}
