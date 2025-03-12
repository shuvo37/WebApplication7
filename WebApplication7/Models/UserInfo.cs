using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{

    [Table("UserInfo")]
    public class UserInfo
    {

              
       
            [Key] public int UserId { get; set; }


            [Required] public string Username { get; set; }

            [Required] public string Email { get; set; }
            [Required] public string Password { get; set; }

            [Required] public string ConfirmPassword { get; set; }


        }
}
