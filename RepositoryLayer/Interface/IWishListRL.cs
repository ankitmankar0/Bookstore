using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        string AddBookinWishList(AddToWishList wishListModel, int userId);
        bool DeleteBookinWishList(int WishListId, int userId);
        List<WishListModel> GetAllBooksinWishList(int UserId);
    }
}
