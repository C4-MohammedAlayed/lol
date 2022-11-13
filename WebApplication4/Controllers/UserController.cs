using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Model;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
        
    {

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        db DB = new db();

        public IConfiguration Configuration { get; }

        [HttpGet]
        public List<User> GetUser()
        {
            List<User> UserLists = new List<User>();
            User user = new User();
            
            DataSet ds = DB.GetUsers(user);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UserLists.Add(new User
                {
                    id = Convert.ToInt32(dr["id"]),
                    UserName = dr["UserName"].ToString(),
                    firstname= dr["firstname"].ToString(),
                    lastname=dr["lastname"].ToString(),
                    Password=dr["Password"].ToString(),
                    dob= dr["dob"].ToString(),
                    role= dr["role"].ToString(),
                    contactno= dr["contactno"].ToString(),
                    gender= dr["gender"].ToString(),
                    Email = dr["Email"].ToString(),
                }); 
            }
            return UserLists;
        }
        [Route("GetUserBYID")]
        [HttpGet("{id}")]
        public List<User> GetUserBYID(int id)
        {
            List<User> UserList = new List<User>();
            User user = new User();
         
            user.id = id;
            DataSet ds = DB.GetUsers(user);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UserList.Add(new User
                {
                    id = Convert.ToInt32(dr["id"]),
                    UserName = dr["UserName"].ToString(),
                    firstname = dr["firstname"].ToString(),
                    lastname = dr["lastname"].ToString(),
                    Password = dr["Password"].ToString(),
                    dob = dr["dob"].ToString(),
                    role = dr["role"].ToString(),
                    contactno = dr["contactno"].ToString(),
                    gender = dr["gender"].ToString(),
                    Email = dr["Email"].ToString(),
                });
            }
            return UserList;
        }

        [Route("AddUser")]
        [HttpPost]
        public string AddUser([FromBody] User user)
           
        {
           string Message = string.Empty;
            try
            {
                Message= DB.AddUser(user);// this function will return message if success or field
                 
            }
            catch (Exception ex)
            {

                Message=ex.Message;
            }
            return Message;
        }


        [Route("Login")]
        [HttpPost]
        public string Login([FromBody] User user)

        {
            string Message = string.Empty;
           

            try
            {
                Message = DB.Login(user);// this function will return message if success or field

                if (Message != "1")
                {
                    return "not Coreect";
                }
               
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
            try
            {  
                var authClaims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name,user.Email),
                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };
                    var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
               issuer: Configuration["JWT:ValidIssuer"],
               audience: Configuration["JWT:ValidAudience"],
               expires: DateTime.Now.AddDays(1),
               claims: authClaims,
               signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature)
               );

                    return new JwtSecurityTokenHandler().WriteToken(token);
                
            }
            catch (Exception )
            {

                throw ;
            }
        }

    }
}
