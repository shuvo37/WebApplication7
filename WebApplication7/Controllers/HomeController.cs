using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using Microsoft.AspNetCore.Authentication;


namespace WebApplication7.Controllers
{

    
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly taskContext _context;

        public HomeController(taskContext context)
        {
            _httpClient = new HttpClient();
            _context = context;
        }

        // Action to display the code submission form
        public async Task<IActionResult> SubmitCode(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var problemInfo = await _context.ProblemInfo.FirstOrDefaultAsync(p => p.Id == id);
            if (problemInfo == null)
            {
                return NotFound();
            }
            return View(problemInfo);

            
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync("CookieAuth");


            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("UserEmail");

            // Redirect to the home page or login page
            return RedirectToAction("Index", "Account");
        }



        public IActionResult Index()
        {
            

           
            return View();
        }

      


        public IActionResult Submission_page()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CompileCode(string language, string code,int Id, string Name ,  string input, string expectedOutput)
        {
            string normalizedInput = NormalizeOutput(input);

            Console.WriteLine($"Language: {language}");
            //Console.WriteLine($"Code: {code}");
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Input: {normalizedInput}");
            Console.WriteLine($"ExpectedOutput: {expectedOutput}");

            var payload = new
            {
                language = language,
                version = "*",
                files = new[]
                 {
                    new
                    {
                        content = code
                    }
                },
                stdin = normalizedInput
            };
            var submission= new Submission();
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://emkc.org/api/v2/piston/execute", content);

            if (response.IsSuccessStatusCode)
            {

                Console.WriteLine($"HERE");

                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + result);

                try
                {
                    var executionResult = JsonSerializer.Deserialize<PistonExecutionResult>(result);

                    if (executionResult?.Run == null)
                    {
                        Console.WriteLine($"HERE1");
                        ViewBag.Error = "Failed to parse API response. The 'run' object is null. Response: " + result;
                    }
                    else if (!string.IsNullOrEmpty(executionResult.Run.Stderr))
                    {
                        Console.WriteLine($"HERE2");
                        ViewBag.Error = executionResult.Run.Stderr;
                    }
                    else
                    {
                        Console.WriteLine($"HERE3");

                        ViewBag.Output = executionResult.Run.Output;

                        // Compare output with expected output
                        bool isAccepted = CompareOutputs(executionResult.Run.Output, expectedOutput);
                        string resultMessage = isAccepted ? "Accepted" : "Rejected";

                        string outputForDatabase = executionResult.Run.Output.Replace("\n", "<br>");
                        // Save submission to the database

                        submission.Pblm_id = Id;
                        submission.UserId = int.Parse(Request.Cookies["UserId"]);
                        submission.Username = Request.Cookies["UserName"];
                        submission.pblm_name = Name;
                        submission.Language = language;
                        submission.Code = code;
                        submission.Input = input;
                        submission.Output = outputForDatabase;
                        submission.ExpectedOutput = expectedOutput;
                        submission.Result = resultMessage;
                        submission.SubmissionTime = DateTime.UtcNow; // Get current UTC time
                        


                        _context.Submission.Add(submission);
                        await _context.SaveChangesAsync();

                        ViewBag.Result = resultMessage;
                    }
                }
                catch (JsonException ex)
                {
                    ViewBag.Error = "Failed to parse API response. Please check the logs. Error: " + ex.Message;
                    Console.WriteLine("JSON Parsing Error: " + ex.Message);
                }
            }
            else
            {
                ViewBag.Error = "Failed to execute code. Please try again.";
            }
            IEnumerable<Submission> submissions = _context.Submission.ToList();

            submissions = _context.Submission
        .OrderByDescending(s => s.SubmissionTime) // Sort by date (latest first)
        .ToList();

            return View("Submission_page" , submissions);
        }
        private static bool CompareOutputs(string userOutput, string expectedOutput)
        {
            // Normalize both outputs
            userOutput = NormalizeOutput(userOutput);
            expectedOutput = NormalizeOutput(expectedOutput);

            // Split outputs into individual components
            var userOutputs = userOutput.Split(new[] { ' ', ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var expectedOutputs = expectedOutput.Split(new[] { ' ', ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Check if the number of components matches
            if (userOutputs.Length != expectedOutputs.Length)
            {
                return false;
            }

            // Compare each component
            for (int i = 0; i < userOutputs.Length; i++)
            {
                if (userOutputs[i] != expectedOutputs[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static string NormalizeOutput(string output)
        {
            if (output == null)
            {
                return string.Empty; // or return null, depending on your use case
            }
            // Replace <br> with \n in the expected output
            output = output.Replace("<br>", "\n");

            // Normalize line endings to \n
            output = output.Replace("\r\n", "\n");

            // Trim leading and trailing whitespace
            output = output.Trim();

            // Replace multiple spaces with a single space
            output = System.Text.RegularExpressions.Regex.Replace(output, @"\s+", " ");

            return output;
        }

    
            private class PistonExecutionResult
            {
                [JsonPropertyName("language")]
                public string Language { get; set; }

                [JsonPropertyName("version")]
                public string Version { get; set; }

                [JsonPropertyName("run")]
                public ExecutionResult Run { get; set; }

                [JsonPropertyName("compile")]
                public ExecutionResult Compile { get; set; }
            }
        

        private class ExecutionResult
        {
            [JsonPropertyName("stdout")]
            public string Stdout { get; set; }

            [JsonPropertyName("stderr")]
            public string Stderr { get; set; }

            [JsonPropertyName("output")]
            public string Output { get; set; }

            [JsonPropertyName("code")]
            public int Code { get; set; }

            [JsonPropertyName("signal")]
            public object Signal { get; set; }
        }

        
    }
}
