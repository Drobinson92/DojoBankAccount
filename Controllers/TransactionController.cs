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
namespace bankaccounts.Controllers{
    public class TransactionController : Controller{
        private UserContext _context;
        public TransactionController(UserContext context){
            _context = context;
        }
        public IActionResult Transactions(int id){
            if(HttpContext.Session.GetInt32("id") == null){
                return RedirectToAction("Index", "Home");
            }
            if(id != (int) HttpContext.Session.GetInt32("id")){
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            User u = _context.Users.Include(a => a.Transactions).SingleOrDefault(a => a.UserId == id);
            ViewBag.u = u;

            return View();
        }
        public IActionResult Transact(Transaction t){
            if(ModelState.IsValid){
            User u = _context.Users.Include(a => a.Transactions).SingleOrDefault(a => a.UserId == (int)HttpContext.Session.GetInt32("id"));
            if((u.Balance + t.Amount) >= 0){
                Transaction newt = new Transaction{
                    Amount = t.Amount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UsersId = u.UserId
                };
                u.Balance += t.Amount;
                _context.Transactions.Add(newt);
                _context.SaveChanges();
                return RedirectToAction("Transactions", new {id = HttpContext.Session.GetInt32("id")});
            }
            }
            // return RedirectToAction("Index");
                return RedirectToAction("Transactions", new {id = HttpContext.Session.GetInt32("id")});
            }
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        }
    }
