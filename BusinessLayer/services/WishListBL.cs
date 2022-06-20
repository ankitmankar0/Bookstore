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

        public string AddBookinWishList(AddToWishList wishListModel)
        {

            try
            {
                return wishlistRL.AddBookinWishList(wishListModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBookinWishList(int WishListId)
        {
            try
            {
                return wishlistRL.DeleteBookinWishList(WishListId);

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
