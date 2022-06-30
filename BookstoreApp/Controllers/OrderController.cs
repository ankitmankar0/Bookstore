using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookstoreApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        
        [HttpPost("Add")]
        public IActionResult AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var cartData = this.orderBL.AddOrder(orderModel, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Order Added SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Order Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [HttpGet("Get")]
        public IActionResult GetAllOrders(int userId)
        {
            try
            {
                //var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var order = this.orderBL.GetAllOrders(userId);
                if (order != null)
                {
                    return this.Ok(new { Status = true, Message = " Successfully Order Details Displayed", Response = order });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Add Login Creditionals To Get Or View Orders" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
