using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookstoreApp.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult AddUser(UserModel user)
        {
            try
            {
                UserModel userData = this.userBL.AddUser(user);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        [HttpPost("login/{Email}/{Password}")]
        public IActionResult LoginUser(string Email, string Password)
        {
            try
            {
                var result = this.userBL.LoginUser(Email, Password);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login Failed", data = result });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var token = this.userBL.ForgotPassword(email);
                if (token != null)
                {
                    return this.Ok(new { Success = true, message = " Mail Sent Successful", Response = token });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("ResetPassword/{newPassword}/{confirmPassword}")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {

                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                //var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Unsuccessfully " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
