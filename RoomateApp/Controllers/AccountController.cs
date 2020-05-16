using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using RoomateApp.Entities;
using RoomateApp.Models;

namespace RoomateApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly RoomateContext _dbContext;

        public AccountController(ILogger<AccountController> logger, RoomateContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = _dbContext.Users.FirstOrDefault();

            return View();
        }

        [HttpPost]
        public ActionResult Index(UserViewModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Users.Add(new Users
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Gender = request.Gender.ToString(),
                        Password = request.Password
                    });

                    _dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("submit sign in failed", ex);
            }

            return View(request);
        }

        public IActionResult UserPreferences(int id)
        {
            var userPref = _dbContext.UserPreferences.FirstOrDefault(up => up.UserId == id);
            
            return View(userPref.ToViewModel());
        }

        [HttpPost]
        public ActionResult UserPreferences(UserPreferencesViewModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUserPref = _dbContext.UserPreferences.FirstOrDefault(up => up.UserId == request.UserId);
                    if (existingUserPref != null)
                    {
                        existingUserPref.AgePreferenceRate = request.AgePreferenceRate;
                        existingUserPref.CleanRate = request.CleanRate;
                        existingUserPref.FoodIssuesRate = request.FoodIssuesRate;
                        existingUserPref.KosherKitchenRate = request.KosherKitchenRate;
                        existingUserPref.MaxPriceRange = request.MaxPriceRange;
                        existingUserPref.MinPriceRange = request.MinPriceRange;
                        existingUserPref.PetFriendlyRate = request.PetFriendlyRate;
                        existingUserPref.ReligiousRate = request.ReligiousRate;
                        existingUserPref.SmokeRate = request.SmokeRate;
                        existingUserPref.SocialFormatRate = request.SocialFormatRate;
                        existingUserPref.GeoLocation = new Point(request.Longitude, request.Latitude) { SRID = 4326 };

                        _dbContext.UserPreferences.Update(existingUserPref);
                    }
                    else
                    {
                        _dbContext.UserPreferences.Add(new UserPreferences
                        {
                            AgePreferenceRate = request.AgePreferenceRate,
                            CleanRate = request.CleanRate,
                            FoodIssuesRate = request.FoodIssuesRate,
                            KosherKitchenRate = request.KosherKitchenRate,
                            MaxPriceRange = request.MaxPriceRange,
                            MinPriceRange = request.MinPriceRange,
                            PetFriendlyRate = request.PetFriendlyRate,
                            ReligiousRate = request.ReligiousRate,
                            SmokeRate = request.SmokeRate,
                            SocialFormatRate = request.SocialFormatRate,
                            UserId = request.UserId
                        });
                    }

                    _dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("submit UserPreferences failed", ex);
            }

            return View(request);
        }
    }
}