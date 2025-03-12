using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Migrations;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {

        private readonly taskContext _context;

        public AccountController(taskContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string Email, string Password)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true
            };

            var user = await _context.UserInfo
           .FirstOrDefaultAsync(u => u.Email == Email);



            if (user == null)
            {

                return NotFound("User does not exist.");
            }

            if (user.Email != Email)
            {
                return RedirectToAction("Index", "Account");
            }

            if (user.Password == Password)
            {
                int UserId = user.UserId;

                string userName = user.Username;

                string UserEmail = user.Email;

                Response.Cookies.Append("UserId", UserId.ToString(), cookieOptions);
                Response.Cookies.Append("UserName", userName, cookieOptions);
                Response.Cookies.Append("UserEmail", UserEmail, cookieOptions);




                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        public IActionResult CreateAccount()
        {
            return View();
        }



        public async Task<IActionResult> CreateAccount1(string Username, string Email, string Password, string ConfirmPassword)
        {
            Console.WriteLine($"Username: {Username}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Password : {Password}");
            Console.WriteLine($"ConfirmPassword: {ConfirmPassword}");
            // Check if Username is empty or too short/long
            if (string.IsNullOrEmpty(Username) || Username.Length < 3 || Username.Length > 20)
            {
                ModelState.AddModelError("Username", "Username must be between 3 and 20 characters.");
                return RedirectToAction("CreateAccount", "Account");
            }

            // Check if Email is empty or does not contain "@gmail.com"
            if (string.IsNullOrEmpty(Email) || !Email.Contains("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email must be a valid Gmail address.");
                return RedirectToAction("CreateAccount", "Account");
            }

            if (Password.Length < 4 || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password must be at least 4 characters.");
                return RedirectToAction("CreateAccount", "Account");
            }
            // Check if Password and ConfirmPassword match
            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return RedirectToAction("CreateAccount", "Account");
            }

            var user = await _context.UserInfo
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {

                return RedirectToAction("CreateAccount", "Account");
            }
            else
            {


                var newUser = new UserInfo
                {
                    Username = Username,
                    Email = Email,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                };
                _context.UserInfo.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");



            }
        }
    }
}