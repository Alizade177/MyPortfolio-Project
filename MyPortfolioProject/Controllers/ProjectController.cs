using Microsoft.AspNetCore.Mvc;
using MyPortfolioProject.Models;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyPortfolioProject.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProjectController(IConfiguration configuration) 
        {
             _configuration = configuration;
        }  

        public IActionResult Projects()
        {

            List<Project> projects = new List<Project>();
            SqlConnection? connection = null;
            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection"));
                SqlCommand command = new SqlCommand("SELECT * FROM Projects", connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Project project = new Project()
                    {
                        Id = (int)dataReader["Id"],
                        ProjectName = (string)dataReader["ProjectName"],
                        ProjectDescription = (string)dataReader["ProjectDescription"],
                        ProjectDate = DateTime.Parse(dataReader["ProjectDate"].ToString()),
                        ProjectURL = (string)dataReader["ProjectURL"],
                        ProjectImageURL = (string)dataReader["ProjectImageURL"],
                    };


                    projects.Add(project);
                  
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " - " + ex.Message + " - " + ex.StackTrace);
            }

            return View(projects);
        }

     
    }
}
