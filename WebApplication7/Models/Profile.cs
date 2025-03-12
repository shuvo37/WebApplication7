using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    [Table("Profile")]
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public UserImg UserImg { get; set; }
        public string Username { get; set; }
        public int TotalSubmissions { get; set; }
        public int SolvedProblems { get; set; }
        public List<Submission> Submission { get; set; }


    }
}
