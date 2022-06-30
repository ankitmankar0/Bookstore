using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookstoreApp.Controllers
{

    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class WishListController : Controller
    {
        private readonly IWishListBL wishlistBL;

        public WishListController(IWishListBL wishListBL)
        {
            this.wishlistBL = wishListBL;
        }

        [HttpPost("addBooksInWishList")]
        public IActionResult AddBookinWishList(AddToWishList wishListModel)
        {
            try
            {
                var result = this.wishlistBL.AddBookinWishList(wishListModel);
                if (result.Equals("book is added in WishList successfully"))
                {
                    return this.Ok(new { success = true, message = $"Book is added in WishList  Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteBookinWishList/{WishListId}")]
        public IActionResult DeleteBookinWishList(int WishListId)
        {
            try
            {
                var result = this.wishlistBL.DeleteBookinWishList(WishListId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book is deleted from the WishList " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllBooksinWishList/{UserId}")]
        public IActionResult GetAllBooksinWishList(int UserId)
        {
            try
            {
                var result = this.wishlistBL.GetAllBooksinWishList(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"All Books Displayed in the WishList Successfully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Books are not there in WishList " });
                }
            }
            catch (Exception eX)
            {
                throw eX;
            }
        }




    }
}
