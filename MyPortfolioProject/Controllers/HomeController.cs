using Microsoft.AspNetCore.Mvc;
using MyPortfolioProject.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MyPortfolioProject.Controllers
{
    public class HomeController : Controller
    {
        private  readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
      public IActionResult Index() 
      {
            List<Skill> skills = new List<Skill>();
            SqlConnection? connection = null;
            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection"));
                SqlCommand command = new SqlCommand("SELECT * FROM Skills", connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    skills.Add(new Skill()
                    {
                        Id = (int)dataReader["Id"],
                        SkillName = (string)dataReader["SkillName"],
                        SkillDescription = (string)dataReader["SkillDescription"],
                        SkillValue = (int)dataReader["SkillValue"]

                    });
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " - " + ex.Message + " - " + ex.StackTrace);
            }
         
             return View(skills);   
      }

        [HttpGet]
        public IActionResult AddSkill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSkill(Skill data)
        {
            /*
            Console.WriteLine(data.SkillName);
            Console.WriteLine(data.SkillDescription);
            Console.WriteLine(data.SkillValue);

            return RedirectToAction("Index", "Home");
            */

           
            SqlConnection? connection = null;
            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection"));
                SqlCommand command = new SqlCommand($"INSERT INTO Skills(SkillName,SkillDescription,SkillValue)  values('{data.SkillName}','{data.SkillDescription}',{data.SkillValue})", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " - " + ex.Message + " - " + ex.StackTrace);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddProjects()
        {
            return View();
        }


        [HttpPost]

        public IActionResult AddProjects(Project data)
        {


            SqlConnection? connection = null;
            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection"));
                SqlCommand command = new SqlCommand($"INSERT INTO Projects(ProjectName,ProjectDescription,ProjectDate,ProjectURL,ProjectImageURL)  values('{data.ProjectName}','{data.ProjectDescription}','{data.ProjectDate}','{data.ProjectURL}','{data.ProjectImageURL}')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " - " + ex.Message + " - " + ex.StackTrace);
            }

            return RedirectToAction("Index", "Home");   
        }


    }
}