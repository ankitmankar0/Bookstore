﻿using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserModel AddUser(UserModel user);

        public UserLogin LoginUser(string Email, string Password);

        public string ForgotPassword(string email);

        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
