using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{

    [Table("UserImg")]
    public class UserImg
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string ImagePath { get; set; }


    }
}
