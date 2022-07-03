using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class WishListBL : IWishListBL
    {
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishlistRL = wishListRL;
        }
        IWishListRL wishlistRL;

        public string AddBookinWishList(AddToWishList wishListModel, int userId)
        {

            try
            {
                return wishlistRL.AddBookinWishList(wishListModel, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBookinWishList(int WishListId, int userId)
        {
            try
            {
                return wishlistRL.DeleteBookinWishList(WishListId, userId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListModel> GetAllBooksinWishList(int UserId)
        {
            try
            {
                return wishlistRL.GetAllBooksinWishList(UserId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
