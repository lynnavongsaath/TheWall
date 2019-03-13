using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Controllers
{
    public class HomeController : Controller
    {
        private wallContext dbContext;
        public HomeController(wallContext context)
        {
            dbContext = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Register")]
        public IActionResult Register(ViewModel submission)
        {
            Users newUser = submission.Register;
            if(ModelState.IsValid)
            {
                PasswordHasher<Users> HashedPW = new PasswordHasher<Users>();
                newUser.Password = HashedPW.HashPassword(newUser, newUser.Password);

                dbContext.Add(newUser);
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Show", "Messages");
            }
            return View("Index");
        }
        [HttpPost("Login")]
        public IActionResult LoginUser(ViewModel submission)
        {
            Login login = submission.Login;
            if(ModelState.IsValid)
            {
                Users UserinDB = dbContext.Users.FirstOrDefault(u => u.Email == login.EmailAttempt);
                if(UserinDB == null)
                {
                    ModelState.AddModelError("Email", "Invalid email or password");
                    return View("Index");
                }
                PasswordHasher<Login> hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(login, UserinDB.Password, login.PasswordAttempt);    
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid email or password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", UserinDB.UserId);
                return RedirectToAction("Show", "Messages");
            }
            return View("Index");
        }
        
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
