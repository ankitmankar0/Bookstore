using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int BooksQty { get; set; }
        public BookModel bookModel { get; set; }
    }
}
