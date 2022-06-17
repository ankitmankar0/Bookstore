using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class CartBL : ICartBL
    {
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        ICartRL cartRL;

        public string AddBookToCart(AddToCart cartBook)
        {
            try
            {
                return this.cartRL.AddBookToCart(cartBook);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteCart(int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CartModel> GetAllBooksinCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBooksinCart(UserId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId, BooksQty);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
