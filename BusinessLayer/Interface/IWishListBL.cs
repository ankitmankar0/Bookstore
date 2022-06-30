using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        string AddBookinWishList(AddToWishList wishListModel);
        bool DeleteBookinWishList(int WishListId);
        List<WishListModel> GetAllBooksinWishList(int UserId);
    }
}
