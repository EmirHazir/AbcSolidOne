using Abc.Entities.Concrete;
using Abc.Services.Abstract;
using Abc.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var productListVM = new ProductListVM
            {
                Products = _productService.GetAll()
            };
            return View(productListVM);
        }

        public ActionResult Insert()
        {
            var model = new ProductAddVM
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Insert(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                TempData.Add("message", "Ürün Başarıyla veritabanına eklendi");
            }

            return RedirectToAction("Insert");
        }


        public ActionResult Update(int productId)
        {
            var model = new ProductUpdateVM
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(product);
                TempData.Add("message", "Ürün Başarıyla veritabanında güncellendi");
            }

            return RedirectToAction("Update");
        }

        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            TempData.Add("message", "Ürün Başarıyla veritabanından silindi");
            return RedirectToAction("Index");
        }



    }
}
