using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookstoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }


        [HttpPost("Add")]
        public IActionResult AddFeedback(FeedBackModel feedbackModeld)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var feedback = this.feedbackBL.AddFeedback(feedbackModeld, userId);
                if (feedback != null)
                {
                    return this.Ok(new { Status = true, Message = " Successfully Feedback For This Book Added ", Response = feedback });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "  Enter Correct BookId!!!!" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("Get")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = this.feedbackBL.GetAllFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Successfully Feedback For Given Book Id Fetched ANd Displayed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


    }
}
