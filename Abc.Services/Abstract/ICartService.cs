using Abc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Services.Abstract
{
    public interface ICartService
    {
        void AddToCart(Cart cart,Product product);
        void RemoveFromCart(Cart cart, int productId);
        List<CartLine> List(Cart cart);

    }
}
