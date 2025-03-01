using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    [Table("mytask")]
    public class taskList
    {
        [Key]
        public int   taskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }

        public DateTime datetime { get; set; }




    }
}
