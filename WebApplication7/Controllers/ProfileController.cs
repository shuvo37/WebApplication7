using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly taskContext _context;

        public ProfileController(taskContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            int userId = Convert.ToInt32(Request.Cookies["UserId"]);

            // Fetch user information
            var user = _context.UserInfo.FirstOrDefault(u => u.UserId == userId);

            // Fetch user image
            var userImg = _context.UserImg.FirstOrDefault(u => u.UserId == userId);

            // If UserImg does not exist, set default image path
            if (userImg == null)
            {
                userImg = new UserImg
                {
                    UserId = userId,
                    ImagePath = "/images/default-profile.jpg" // Default image path
                };

                _context.UserImg.Add(userImg);
                await _context.SaveChangesAsync();
            }

            // Fetch the username from Submission table
            string username = user.Username; /*_context.Submission
                .Where(s => s.UserId == userId)
                .Select(s => s.Username)
                .FirstOrDefault()*/;

            // Fetch total number of submissions
            int totalSubmissions = _context.Submission
                .Count(s => s.UserId == userId);

            // Fetch the number of solved problems (distinct problems with accepted status)
            int solvedProblems = _context.Submission
                .Where(s => s.UserId == userId && s.Result == "Accepted")
                .Select(s => s.Pblm_id)
                .Distinct()
                .Count();

            // Fetch the latest submissions
            var latestSubmissions = _context.Submission
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.SubmissionTime)
                .Take(5)
                .Select(s => new Submission
                {
                    Id = s.Id,
                    Pblm_id = s.Pblm_id,
                    pblm_name = _context.problems1
                        .Where(p => p.PblmId == s.Pblm_id)
                        .Select(p => p.Title)
                        .FirstOrDefault(), // Fetch problem name
                    Language = s.Language,
                    SubmissionTime = s.SubmissionTime,
                    Result = s.Result
                })
                .ToList();

            // Create the Profile object
            var profile = new Profile
            {
                UserImg = userImg,
                Username = username, // Add username
                TotalSubmissions = totalSubmissions, // Add total submissions
                SolvedProblems = solvedProblems, // Add solved problems
                Submission = latestSubmissions
            };

            return View(profile);
        }



        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                int userId = Convert.ToInt32(Request.Cookies["UserId"]);

                // Generate unique filename
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profiles");

                // Ensure the folder exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, fileName);

                // Save image to wwwroot/images/profiles/
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                // Check if user already has an image
                var existingUserImg = _context.UserImg.FirstOrDefault(u => u.UserId == userId);

                if (existingUserImg != null)
                {
                    // Delete old image file if it exists (optional but recommended)
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingUserImg.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    // Update existing record
                    existingUserImg.ImagePath = "/images/profiles/" + fileName;
                    _context.UserImg.Update(existingUserImg);
                }
                else
                {
                    // Insert new record if no existing profile picture
                    var newUserImg = new UserImg
                    {
                        UserId = userId,
                        ImagePath = "/images/profiles/" + fileName
                    };

                    _context.UserImg.Add(newUserImg);
                }

                await _context.SaveChangesAsync();

                TempData["AlertMessage"] = "Profile image updated successfully!";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["AlertMessage"] = "Please select an image!";
                TempData["AlertType"] = "error";
            }

            return RedirectToAction("Index", "Profile");
        }


    }
}
