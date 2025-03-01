using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    [Table("PblmList")]
    public class problems1
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Prevent auto-increment
        public int PblmId { get; set; }
        public string Difficulty { get; set; }
        public int SolvedBy { get; set; }
        public double SuccessRate { get; set; }
        public string Title { get; set; }

        public int TimeLimit { get; set; }

        public int MemoryLimit { get; set; }

        public int WrongTry { get; set; }

        public int WeightLimit { get; set; }

        public int Accepted { get; set; }

    }
}
