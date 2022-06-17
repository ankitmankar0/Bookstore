using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        string AddBookinWishList(AddToWishList wishListModel);
        bool DeleteBookinWishList(int WishListId);
    }
}
