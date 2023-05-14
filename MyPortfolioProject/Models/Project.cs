namespace MyPortfolioProject.Models
{
    public class Project
    {
        public int Id { get; set; } 
        public string ProjectName { get; set; }    

        public string ProjectDescription{ get; set; }

        public DateTime ProjectDate { get; set; }

        public string? ProjectURL { get; set; }  

        public string? ProjectImageURL { get; set; } 
    }
}
