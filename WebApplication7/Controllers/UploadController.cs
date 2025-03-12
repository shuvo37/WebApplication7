using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    public class UploadController : Controller
    {



        public IActionResult Index()
        {
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Uploads(IFormFile file , IFormFile file1)
        {
            if (file == null || file.Length == 0  || file1 == null || file1.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            string fileContent , fileContent1;

            using (var stream = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                // Read entire file as a string
                fileContent = await stream.ReadToEndAsync();
            }

            using (var stream = new StreamReader(file1.OpenReadStream(), Encoding.UTF8))
            {
                // Read entire file as a string
                fileContent1 = await stream.ReadToEndAsync();
            }


            // Trim any extra spaces and replace newlines with <br>
            string formattedContent = fileContent.Trim().Replace("\r\n", "<br>").Replace("\n", "<br>");
            string formattedContent1 = fileContent1.Trim().Replace("\r\n", "<br>").Replace("\n", "<br>");

            Console.WriteLine($"input: {formattedContent}");
            Console.WriteLine($"Expected Output: {formattedContent1}");

            // Example response (you can save formattedContent to the database instead)
            return Index();
        }
    }
}
