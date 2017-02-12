using Abc.Entities.Concrete;
using Abc.Services.Abstract;
using Abc.WebUI.Models;
using Abc.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.WebUI.Controllers
{
    public class CartController : Controller
    {

        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        private IProductService _productService;
        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _cartSessionService = cartSessionService;
            _productService = productService;
        }

        public ActionResult AddToCart(int productId)
        {
            var productToAdded = _productService.GetById(productId);

            var cart = _cartSessionService.GetCart();

            _cartService.AddToCart(cart, productToAdded);

            _cartSessionService.SetCart(cart);

            TempData.Add("message", String.Format("Ürününüz {0} başarıyla eklendi!", productToAdded.ProductName));

            return RedirectToAction("Index", "Product");
        }


        public ActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            CartListVM cartListVm = new CartListVM()
            {
                Cart = cart
            };
            return View(cartListVm);
        }

        public ActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);

            TempData.Add("message", "Ürününüz başarıyla silindi!");
            return RedirectToAction("List");
        }


        public ActionResult Complete()
        {
            var shippingDetailsVM = new ShippingDetailsVM
            {
                ShippingDetails = new ShippingDetails()
            };
            return View(shippingDetailsVM);
        }

        [HttpPost]
        public ActionResult Complete(ShippingDetails shippingDetails)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message",String.Format("Teşekkürler {0}"+" "+"{1}! Siparişiniz hazırlanıyor", shippingDetails.FirstName, shippingDetails.FirstName));
            return View();
        }

    }
}
