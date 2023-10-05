using Microsoft.AspNetCore.Mvc;
using System;
using grades.Models;
using MySql.Data.MySqlClient;
using grades.DTO;

namespace grades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
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

                while(reader.Read())
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

        [HttpGet("{Id}")]
        public ActionResult<GradeDto> GetDbById(Guid Id)
        {
            try
            {
                connect.connection.Open();

                string sql = $"SELECT * FROM `users` WHERE `users`.`Id` = @id";
                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                cmd.Parameters.AddWithValue("id", Id);
                MySqlDataReader reader = cmd.ExecuteReader();

                var userById = new GradeDto(
                        reader.GetGuid(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3)
                        );

                connect.connection.Close();

                return StatusCode(200, userById);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<GradeDto> AddGrade(CreateGrade createGrade)
        {
            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var grade = new Grade
            {
                Id = Guid.NewGuid(),
                GradeT = createGrade.GradeT,
                DescriptionT = createGrade.DescriptionT,
                Created = time
            };

            try
            {
                connect.connection.Open();

                string sql = $"INSERT INTO `grades` (`Id`, `GradeT`, `DescriptionT`, `Created`) VALUES ('{grade.Id}', '{grade.GradeT}', '{grade.DescriptionT}', '{grade.Created}')";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                cmd.ExecuteNonQuery();

                connect.connection.Close();

                return StatusCode(201, sql);
            }
            catch (Exception e)
            {
                return StatusCode(400, e);
            }
        }
    }
}
