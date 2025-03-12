using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    [Table("Submission")]
    public class Submission
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("PblmList")]
        public int Pblm_id { get; set; }
        public int ? UserId { get; set; }
        public string? Username { get; set; }
        public string? pblm_name { get; set; }  // This allows NULL values


        [Required]
        public string Language { get; set; }

        [Required]
        public string Code { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public string ExpectedOutput { get; set; }

        public string Result { get; set; } // Accepted or Rejected

        public DateTime SubmissionTime { get; set; } = DateTime.UtcNow;

    }
}
