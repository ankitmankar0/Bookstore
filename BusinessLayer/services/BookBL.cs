using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class BookBL : IBookBL
    {
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        IBookRL bookRL;

        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
