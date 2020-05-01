using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoomateApp.Entities;
using RoomateApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomateContext _dbContext;

        public ApartmentController(ILogger<HomeController> logger, RoomateContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ApartmentViewModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rooms = new List<RoomDetails>
                    {
                        new RoomDetails
                        {
                            PrivateBalcony = request.RoomDetails.IsPrivateBalcony,
                            PrivateShower = request.RoomDetails.IsPrivateShower,
                            PrivateToilet = request.RoomDetails.IsPrivateToilet,
                            RoomRent = request.RoomDetails.RoomRentPrice,
                            RoomSize = request.RoomDetails.RoomRentSize,
                            StayedFurniture = request.RoomDetails.StayedFurniture
                        } 
                    };
                    var preferences = new ApartmentPreferences
                    {
                        AgePreferenceRate = request.Preferences.AgePreferenceRate,
                        CleanRate = request.Preferences.CleanRate,
                        FoodIssuesRate = request.Preferences.FoodIssuesRate,
                        KosherKitchenRate = request.Preferences.KosherKitchenRate,
                        PetFriendlyRate = request.Preferences.PetFriendlyRate,
                        ReligiousRate = request.Preferences.ReligiousRate,
                        SmokeRate = request.Preferences.SmokeRate,
                        SocialFormatRate = request.Preferences.SocialFormatRate
                    };
                    _dbContext.Apartment.Add(new Apartment
                    {
                        UserId = 1,
                        City = "",
                        Street = "",
                        AdditionalComments = request.AdditionalComments,
                        AvailableRooms = request.AvailableRoomsCount,
                        Floor = request.Floor,
                        HasLift = request.HasLift,
                        HasLivingroom = request.HasLivingroom,
                        HasParking = request.HasParking,
                        HouseholdPrice = request.HouseholdPrice,
                        LeaseStartDate = request.LeaseStartDate,
                        TaxPrice = request.TaxPrice,
                        RoomsCount = request.RoomsCount,
                        RoomDetails = rooms,
                        ApartmentPreferences = preferences
                    });

                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("submit login failed", ex);
            }

            return View(request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
