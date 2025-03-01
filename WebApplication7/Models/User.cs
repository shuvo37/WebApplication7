using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    [Table("User")]
    public class User
    {

        [Key] public int Id { get; set; }


        [Required] public string Username { get; set; }

        [Required] public string Email {get; set; }
        [Required] public string Password { get; set; }

        [Required] public string ConfirmPassword { get; set; }



    }
}
