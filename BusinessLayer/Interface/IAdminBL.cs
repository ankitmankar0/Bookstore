﻿using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public AdminLoginModel Adminlogin(AdminResponse adminResponse);
    }
}
