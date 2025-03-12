using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    [Table("PblmDescription")]
    public class ProblemInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Prevent auto-increment
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200)]
        public string Title { get; set; }

        public int? UserId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Input format is required.")]
        public string InputFormat { get; set; }

        [Required(ErrorMessage = "Output format is required.")]
        public string OutputFormat { get; set; }

        [Required(ErrorMessage = "Input example cannot be empty.")]
        public string InputExample { get; set; }

        [Required(ErrorMessage = "Output example cannot be empty.")]
        public string OutputExample { get; set; }

        public string? Explanation { get; set; }
        
    }
}
