using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        string AddBookToCart(AddToCart cartBook);
        bool UpdateCart(int CartId, int BooksQty);
        string DeleteCart(int CartId);
        List<CartModel> GetAllBooksinCart(int UserId);
    }
}
