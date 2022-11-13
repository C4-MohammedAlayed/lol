using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Model
{
    public class db
    {
        public db()
        {
        }

        public db(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }
        public IConfiguration Configuration { get; }
         SqlConnection con =  new SqlConnection("Server = 192.168.60.146; Database=test;Integrated Security = True");


        public string AddUser(User user)
        {
            string Message = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("SaveUsers", con);// first parameter is the name of strodProsduer

                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", user.id);
                com.Parameters.AddWithValue("@UserName", user.UserName);
                com.Parameters.AddWithValue("@firstName", user.firstname);
                com.Parameters.AddWithValue("@lastName", user.lastname);
                com.Parameters.AddWithValue("@Email", user.Email);
                com.Parameters.AddWithValue("@Password", user.Password);
                com.Parameters.AddWithValue("@dob", user.dob);
                com.Parameters.AddWithValue("@role", user.role);
                com.Parameters.AddWithValue("@contactno", user.contactno);
                com.Parameters.AddWithValue("@gender", user.gender);
                
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                Message = "success";
            }
            catch (Exception ex)
            {

             Message=   ex.Message;
            }
            finally
            {
                if (con.State== System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Message;
        }

        // Get
        public DataSet GetUsers(User user)
        {
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("GetUsers", con);// first parameter is the name of strodProsduer

                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", user.id);
                com.Parameters.AddWithValue("@UserName", user.UserName);
                com.Parameters.AddWithValue("@firstName", user.firstname);
                com.Parameters.AddWithValue("@lastName", user.lastname);
                com.Parameters.AddWithValue("@Email", user.Email);
                com.Parameters.AddWithValue("@Password", user.Password);
                com.Parameters.AddWithValue("@dob", user.dob);
                com.Parameters.AddWithValue("@role", user.role);
                com.Parameters.AddWithValue("@contactno", user.contactno);
                com.Parameters.AddWithValue("@gender", user.gender);
                
               
                 SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                Message = "success";
               
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
            return ds;
        }

        //Login
        public string Login(User user)
        {
            string Message = string.Empty;
            DataSet ds = new DataSet();
            int status=1;
            try
            {
                SqlCommand com = new SqlCommand("Login", con);// first parameter is the name of strodProsduer

                com.CommandType = System.Data.CommandType.StoredProcedure;             
                com.Parameters.AddWithValue("@Email", user.Email);
                com.Parameters.AddWithValue("@Password", user.Password);


                con.Open();
                Message = com.ExecuteNonQuery().ToString();
                status = Convert.ToInt16(com.ExecuteScalar());
                if (status == 1)
                {
                    return status.ToString();
                }
                                         // will return 0 or 1
                con.Close();
              


            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
             return status.ToString();

        }

        public string CreateChampion(Champion champion)
        {
            string Message = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("SpChap", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", champion.id);
                com.Parameters.AddWithValue("@name", champion.name);
                com.Parameters.AddWithValue("@picture", champion.picture);
                com.Parameters.AddWithValue("@description", champion.description);
                com.Parameters.AddWithValue("@tier", champion.tier);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Message;
        }

        public DataSet GetChampions(Champion champion)
        {
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("getChampions", con);
               /* com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", champion.id);
                com.Parameters.AddWithValue("@name", champion.name);
                com.Parameters.AddWithValue("@picture", champion.picture);
                com.Parameters.AddWithValue("@description", champion.description);
                com.Parameters.AddWithValue("@tier", champion.tier);*/
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {

                Message = ex.Message;
               
            }
            return ds;
        }
    }
}
