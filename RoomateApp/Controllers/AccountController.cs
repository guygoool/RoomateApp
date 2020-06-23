using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using RoomateApp.Entities;
using RoomateApp.Models;

namespace RoomateApp.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly RoomateContext _dbContext;

        public AccountController(ILogger<AccountController> logger, RoomateContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Index(UserViewModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _dbContext.Users.Add(new Users
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Gender = request.Gender.ToString(),
                        Password = request.Password,
                        UserLogin = request.UserLogin
                    }).Entity;
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home", new { userId = newUser.Id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("submit sign in failed", ex);
            }

            return View(request);
        }
    }
}