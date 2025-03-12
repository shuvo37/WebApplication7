using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{

    [Table("TestCase")]
    public class TestCase
    {


        public int Id { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }

    }
}
