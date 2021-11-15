using DemoApp.Data.DataModels;
using DemoApp.Data.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DemoApp.Data.Repository
{
    public class UserRepository
    {
        private string _dbConn;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public UserRepository()
        {
            _dbConn = System.Configuration.ConfigurationManager.ConnectionStrings["appDbConn"].ConnectionString;
        }

        public bool AddUser(UserViewModel model)
        {
            try
            {
                model.Password = textEncode(model.Password);

                string query = "Insert Into dbo.Users (GUID, EmailAddress, Password) " +
                                   "VALUES (@guid, @emailAddress, @password) ";

                using (SqlConnection cn = new SqlConnection(_dbConn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@guid", System.Data.SqlDbType.NVarChar, 128).Value = Guid.NewGuid().ToString();
                    cmd.Parameters.Add("@emailAddress", System.Data.SqlDbType.NVarChar, 100).Value = model.EmailAddress;
                    cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 100).Value = model.Password;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }

                return true;

            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            return false;
        }

        public UserViewModel GetUser(string guid)
        {
            UserViewModel model = new UserViewModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConn))
                {
                    string query = @"SELECT e.guid, e.emailaddress, e.password FROM users e where e.guid = '" + guid + "'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.GUID = Convert.ToString(dr.GetString(0));
                            model.EmailAddress = Convert.ToString(dr.GetString(1));
                            model.Password = textDecode(Convert.ToString(dr.GetString(2)));
                        }
                    }

                    dr.Close();
                    conn.Close();
                }

                return model;
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            return null;
        }

        public List<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> usersList = new List<UserViewModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConn))
                {
                    string query = @"SELECT e.guid, e.emailaddress, e.password FROM users e";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usersList.Add(new UserViewModel
                            {

                                GUID = Convert.ToString(dr.GetString(0)),
                                EmailAddress = Convert.ToString(dr.GetString(1)),
                                Password = textDecode(Convert.ToString(dr.GetString(2))),
                            });
                        }
                    }

                    dr.Close();
                    conn.Close();
                }

                return usersList;
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            return null;
        }

        public bool UpdateUser(UserViewModel model)
        {
            try
            {
                model.Password = textEncode(model.Password);

                using (SqlConnection conn = new SqlConnection(_dbConn))
                {
                    string query = @"update users set emailaddress= '" + model.EmailAddress + "' , password= '" + model.Password + "' where guid = '" + model.GUID + "'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    var result = cmd.ExecuteNonQuery();

                    conn.Close();

                    if (result == 1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            return false;
        }

        public bool DeleteUser(string guid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConn))
                {
                    string query = @"DELETE FROM users where guid = '" + guid + "'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    var result = cmd.ExecuteNonQuery();

                    conn.Close();

                    if (result == 1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            return false;
        }

        public bool CheckEmailExist(string emailAddress)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConn))
                {
                    string query = @"SELECT e.emailaddress FROM users e where e.emailaddress = '" + emailAddress + "'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        return true;

                    dr.Close();
                    conn.Close();
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }
            return false;
        }

        public string textEncode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                logger.Info(ex);
                throw new Exception("Error unable to encode data");
            }
        }
        public string textDecode(string data)
        {
            try
            {
                var encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                logger.Info(ex);
                throw new Exception("Error unable to decode data");
            }
        }
    }
}