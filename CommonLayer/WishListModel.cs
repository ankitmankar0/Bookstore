﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class WishListModel
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public BookModel bookModel { get; set; }
    }
}
