using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);

        public BookModel GetBookByBookId(int BookId);
        public bool DeleteBook(int BookId);
        bool UpdateBook(int BookId, BookModel updateBook);
        public List<BookModel> GetAllBooks();
    }
}
