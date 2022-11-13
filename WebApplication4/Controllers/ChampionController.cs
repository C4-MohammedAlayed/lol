using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Model;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionController : ControllerBase
    {
        public ChampionController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        db DB = new db();

        public IConfiguration Configuration { get; }



        [Route("AddChampion")]
        [HttpPost]
        public string CreateChampion([FromBody] Champion champion)
        {

            string Message = string.Empty;
            try
            {
                Message = DB.CreateChampion(champion);
            }
            catch (Exception ex)
            {
                Message = ex.Message; ;

            }
            return Message;
        }

        [Route("GetChampions")]
        [HttpGet]
        public List<Champion> GetChampions()
         
            {
            Champion champion = new Champion();
            List<Champion> Champions = new List<Champion>();
            DataSet ds = DB.GetChampions(champion);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Champions.Add(new Champion
                {
                    id = Convert.ToInt32(dr["id"]),
                    name = dr["name"].ToString(),
                    picture = dr["picture"].ToString(),
                    description = dr["description"].ToString(),
                    tier = dr["tier"].ToString(),
                });
            }
            return Champions;
        }

    }

}
