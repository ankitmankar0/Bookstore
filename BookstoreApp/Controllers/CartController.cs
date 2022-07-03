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
    public class CartController : Controller
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddBookToCart")]
        public IActionResult AddToCart(AddToCart cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);             
                var userData = this.cartBL.AddBookToCart(cart, userId);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added to cart Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Book Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("Delete/{CartId}")]
        public IActionResult DeletCart(int CartId)
        {
            try
            {
                var data = this.cartBL.DeleteCart(CartId);
                if (data != null)
                {
                    return this.Ok(new { Success = true, message = "Book in Cart Deleted Sucessfully", });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid CartId" }); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBooksInCart")]
        public IActionResult GetAllBookInCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                var result = this.cartBL.GetAllBooksinCart(userId);

                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "All Books Displayed in the cart Successfully", Response = result });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid UserId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateCart/{CartId}/{BooksQty}")]
        public IActionResult UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                var Data = this.cartBL.UpdateCart(CartId, BooksQty);
                if (Data == true)
                {
                    return this.Ok(new { Success = true, message = " Book Updated successfully", Response = Data });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid BookId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
