using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SomeCompany.Data;
using SomeCompany.Models;
using System.Diagnostics;

namespace SomeCompany.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly SignInManager<UserModel> _signInManager;
        public HomeController(AppDbContext dbContext, SignInManager<UserModel> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = _dbContext.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult Block([FromForm(Name = "userIds[]")] string[] IDs)
        {
            var users = _dbContext.Users.ToList();
            var Ids = IDs;

            foreach(var id in Ids)
            {
                var user = users.Find(x => x.Id == id);
                user.IsBlocked = true;

                _dbContext.Update(user);
                _dbContext.SaveChanges();
            }

            return Json(new { success = true, message = "User blocked successfully" });
        }

        [HttpPost]
        public IActionResult UnBlock([FromForm(Name = "UserIds[]")] string[] IDs)
        {
            var users = _dbContext.Users.ToList();
            var Ids = IDs;

            foreach(var id in Ids)
            {
                var user = users.Find(x => x.Id == id);
                user.IsBlocked = false;

                _dbContext.Update(user);
                _dbContext.SaveChanges();
            }
            return Json(new { success = true, message = "User unblocked successfully" });
        }

        //[HttpPost]
        //public IActionResult UnBlockAll([FromForm(Name = "UserIds[]")] string[] IDs)
        //{
        //    var users = _dbContext.Users.ToList();

        //    foreach (var user in users)
        //    {
        //        user.IsBlocked = true;
        //        _dbContext.Update(user);
        //        _dbContext.SaveChanges();
        //    }
        //    return Json(new { success = true, message = "User unblocked successfully" });
        //}

        [HttpPost]
        public IActionResult Delete([FromForm(Name = "UserIds[]")] string[] IDs)
        {
            var users = _dbContext.Users.ToList();
            var Ids = IDs;

            foreach(var id in Ids)
            {
                var user = users.Find(x => x.Id == id);
                _dbContext.Remove(user);
                _dbContext.SaveChanges();
            }

            return Json(new { success = true, message = "User unblocked successfully" });
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();

            return Json(new { success = true, message = "" });
        }
    }
}
