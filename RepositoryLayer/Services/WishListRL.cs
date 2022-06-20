using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public string AddBookinWishList(AddToWishList wishListModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("spAddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", wishListModel.UserId);
                cmd.Parameters.AddWithValue("@BookId", wishListModel.BookId);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return "book is added in WishList successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteBookinWishList(int WishListId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("spDeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@WishListId", WishListId);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<WishListModel> GetAllBooksinWishList(int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("spGetAllBooksinWishList", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<WishListModel> wishList = new List<WishListModel>();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WishListModel model = new WishListModel();
                        BookModel bookModel = new BookModel();
                        model.UserId = Convert.ToInt32(reader["UserId"]);
                        model.WishListId = Convert.ToInt32(reader["WishListId"]);
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                        bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetails = reader["BookDetails"].ToString();
                        bookModel.BookImage = reader["BookImage"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                        model.BookId = Convert.ToInt32(reader["BookId"]);
                        model.bookModel = bookModel;
                        wishList.Add(model);
                    }
                    return wishList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
