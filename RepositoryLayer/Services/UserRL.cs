using CommonLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public UserModel AddUser(UserModel user)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("spUserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                var passwordToEncrypt = EncodePasswordToBase64(user.Password);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", passwordToEncrypt);
                //cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public UserLogin LoginUser(string Email, string Password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand com = new SqlCommand("spUserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //Command type is a class to set as stored procedure
                };

                var passwordToEncrypt = EncodePasswordToBase64(Password);
                com.Parameters.AddWithValue("@Email", Email);
                com.Parameters.AddWithValue("@Password", passwordToEncrypt);

                this.sqlConnection.Open();
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    int UserId = 0;
                    UserLogin user = new UserLogin();
                    while (rdr.Read()) //using while loop for read multi
                    {
                        user.Email = Convert.ToString(rdr["Email"]);
                        user.Password = Convert.ToString(rdr["Password"]);
                        UserId = Convert.ToInt32(rdr["UserId"]);
                    }
                    this.sqlConnection.Close();
                    user.Token = this.GetJWTToken(user.Email, UserId);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();  //Always ensuring the closing of the connection
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand com = new SqlCommand("spUserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);

                this.sqlConnection.Open();

                SqlDataReader reader = com.ExecuteReader(); // Execute sqlDataReader to fetching all records
                if (reader.HasRows)  // Checking datareader has rows or not.    
                {
                    int userId = 0;
                    UserLogin user = new UserLogin();
                    while (reader.Read()) //using while loop for read multiple rows.
                    {
                        email = Convert.ToString(reader["Email"]);
                        userId = Convert.ToInt32(reader["UserId"]);
                    }
                    this.sqlConnection.Close();

                    MessageQueue queue;

                    if (MessageQueue.Exists(@".\Private$\BookQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\BookQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = this.GetJWTToken(email, userId);
                    EmailService.SendMail(email, MyMessage.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReciveCompleted);

                    var token = this.GetJWTToken(email, userId);

                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                    SqlCommand com = new SqlCommand("spUserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure //Command type is a class to set as stored procedure
                    };

                    // var encryptPassword = EncodePasswordToBase64(newPassword);


                    com.Parameters.AddWithValue("@Email", email);
                    //com.Parameters.AddWithValue("@Password", EncodePasswordToBase64(newPassword));
                    com.Parameters.AddWithValue("@Password", newPassword);

                    this.sqlConnection.Open();
                    var result = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }



        //Implementing Jwt Token For Login using Email and Id
        public string GetJWTToken(string email, long userID)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("email", email),
                    new Claim("userID",userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new string(decoded_char);
            return result;
        }


        private void msmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender;
                    Message msg = queue.EndReceive(e.AsyncResult);
                    EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                    queue.BeginReceive();
                }

            }
            catch (MessageQueueException ex)
            {

                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        }

        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
