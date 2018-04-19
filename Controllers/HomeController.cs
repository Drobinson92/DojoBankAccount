using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bankaccounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace bankaccounts.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;
        public HomeController(UserContext context){
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginPage(){
            return View("Login");
        }

        public IActionResult Register(UserViewModel user){
            if(ModelState.IsValid){
                PasswordHasher<UserViewModel> Hasher = new PasswordHasher<UserViewModel>();
                user.Password = Hasher.HashPassword(user, user.Password);
                User u = new User{
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Balance =  0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                // HttpContext.Session.SetString("email", u.Email);
                _context.Add(u);
                _context.SaveChanges();
                User newUser = _context.Users.SingleOrDefault(a => a.Email == user.Email);
                HttpContext.Session.SetInt32("id", newUser.UserId);
                return RedirectToAction("Transactions", "Transaction", new {id = HttpContext.Session.GetInt32("id")});
            }
            return View("Index");
        }
        public IActionResult Login(LoginViewModel user){
            // List<User> u = _context.Users.Where(a => a.Email == user.Email).ToList();
            if(ModelState.IsValid){

            User u = _context.Users.SingleOrDefault(a => a.Email == user.Email);
            if(u != null){
            if(user.Email != null && user.Password != null){
                var Hasher = new PasswordHasher<LoginViewModel>();
                if(0 != Hasher.VerifyHashedPassword(user, u.Password, user.Password)){
                    HttpContext.Session.SetInt32("id", u.UserId);
                    return RedirectToAction("Transactions", "Transaction", new {id = HttpContext.Session.GetInt32("id")});
                }
            }
            }
            }
            return View("Login");
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
