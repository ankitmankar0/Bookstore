using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        string AddBookToCart(AddToCart cartBook, int userId);
        bool UpdateCart(int CartId, int BooksQty);
        string DeleteCart(int CartId);
        List<CartModel> GetAllBooksinCart(int UserId);
    }
}
