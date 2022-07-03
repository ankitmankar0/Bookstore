using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        [Authorize(Roles = Role.User)]
        [HttpPost("addBooksInWishList")]
        public IActionResult AddBookinWishList(AddToWishList wishListModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.wishlistBL.AddBookinWishList(wishListModel, userId);
                if (result.Equals("book is added in WishList successfully"))
                {
                    return this.Ok(new { success = true, message = $"Book is added in WishList  Successfully" });
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
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.wishlistBL.DeleteBookinWishList(WishListId, userId);
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

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBooksinWishList")]
        public IActionResult GetAllBooksinWishList()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.wishlistBL.GetAllBooksinWishList(userId);
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
