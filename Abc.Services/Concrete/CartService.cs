using Abc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Entities.Concrete;

namespace Abc.Services.Concrete
{
    public class CartService : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartline = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartline != null)
            {
                cartline.Quantity++;
                return;
            }
            else
            {
                cart.CartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity =1
                });
            }
        }

        public List<CartLine> List(Cart cart)
        {
           return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId));
        }

       
    }
}
