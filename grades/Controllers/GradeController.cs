using Microsoft.AspNetCore.Mvc;
using System;
using grades.Models;
using static grades.Controllers.DTO;
using MySql.Data.MySqlClient;

namespace grades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : Controller
    {
        Connect connect = new();
        private readonly List<GradeDto> grades = new();

        [HttpGet]
        public ActionResult<IEnumerable<GradeDto>> GetDb()
        {
            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM `grades`";
                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                foreach (var data in reader)
                {
                    var result = new GradeDto(
                        reader.GetGuid(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3)
                        );
                    grades.Add(result);
                }

                connect.connection.Close();

                return StatusCode(200, grades);
            }
            catch (Exception e)
            {
                return StatusCode(503, e);
            }
        }
    }
}
