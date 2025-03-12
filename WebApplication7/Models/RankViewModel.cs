using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    [Table("Rank")]
    public class RankViewModel
    {
       public int Id { get; set; }
        public int Rank { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SolvedProblems { get; set; }
        public int TotalRejected { get; set; }

    }
}
