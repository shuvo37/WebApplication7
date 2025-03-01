using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;

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
            var user = await _context.User
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
                return  RedirectToAction("Index", "Home");
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
    }
}
