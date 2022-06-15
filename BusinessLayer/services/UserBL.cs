using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class UserBL : IUserBL
    {
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        IUserRL userRL;
        public UserModel AddUser(UserModel user)
        {
            try
            {
                return this.userRL.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserLogin LoginUser(string Email, string Password)
        {
            try
            {
                return this.userRL.LoginUser(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
