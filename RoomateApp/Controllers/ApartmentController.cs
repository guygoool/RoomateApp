using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using RoomateApp.Entities;
using RoomateApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Controllers
{
    [Route("[controller]")]
    public class ApartmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomateContext _dbContext;

        public ApartmentController(ILogger<HomeController> logger, RoomateContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public IActionResult Index(int userId)
        {
            return View(new ApartmentViewModel() { UserId = userId });
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> Index(ApartmentViewModel request, int userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var apartment = await _dbContext.Apartment.FirstOrDefaultAsync(c => c.Id == request.Id);
                    if (apartment != null)
                    {
                        var roomDetails = apartment.RoomDetails.FirstOrDefault();
                        if (roomDetails != null)
                        {
                            roomDetails.PrivateBalcony = request.RoomDetails.IsPrivateBalcony;
                            roomDetails.PrivateShower = request.RoomDetails.IsPrivateShower;
                            roomDetails.PrivateToilet = request.RoomDetails.IsPrivateToilet;
                            roomDetails.RoomRent = request.RoomDetails.RoomRentPrice;
                            roomDetails.RoomSize = request.RoomDetails.RoomRentSize;
                            roomDetails.StayedFurniture = request.RoomDetails.StayedFurniture;
                        }
                        else
                        {
                            apartment.RoomDetails.Add(new RoomDetails
                            {
                                ApartmentId = apartment.Id,
                                PrivateBalcony = request.RoomDetails.IsPrivateBalcony,
                                PrivateShower = request.RoomDetails.IsPrivateShower,
                                PrivateToilet = request.RoomDetails.IsPrivateToilet,
                                RoomRent = request.RoomDetails.RoomRentPrice,
                                RoomSize = request.RoomDetails.RoomRentSize,
                                Status = "Available",
                                StayedFurniture = request.RoomDetails.StayedFurniture
                            });
                        }

                        var preferences = apartment.ApartmentPreferences;
                        if (preferences != null)
                        {
                            preferences.AgePreferenceRate = request.Preferences.AgePreferenceRate;
                            preferences.CleanRate = request.Preferences.CleanRate;
                            preferences.FoodIssuesRate = request.Preferences.FoodIssuesRate;
                            preferences.KosherKitchenRate = request.Preferences.KosherKitchenRate;
                            preferences.PetFriendlyRate = request.Preferences.PetFriendlyRate;
                            preferences.ReligiousRate = request.Preferences.ReligiousRate;
                            preferences.SmokeRate = request.Preferences.SmokeRate;
                            preferences.SocialFormatRate = request.Preferences.SocialFormatRate;
                        }
                        else
                        {
                            apartment.ApartmentPreferences = new ApartmentPreferences
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
                        }

                        apartment.UserId = request.UserId;
                        apartment.AdditionalComments = request.AdditionalComments;
                        apartment.ApartmentPreferences = preferences;
                        apartment.AvailableRooms = request.AvailableRoomsCount;
                        apartment.Floor = request.Floor;
                        apartment.HasLift = request.HasLift;
                        apartment.HasLivingroom = request.HasLivingroom;
                        apartment.HasParking = request.HasParking;
                        apartment.HouseholdPrice = request.HouseholdPrice;
                        apartment.LeaseStartDate = request.LeaseStartDate;
                        apartment.RoomDetails = new List<RoomDetails> { roomDetails };
                        apartment.RoomsCount = request.RoomsCount;
                        apartment.Status = "Available";
                        apartment.TaxPrice = request.TaxPrice;
                        apartment.GeoLocation = new Point(request.Longitude, request.Latitude) { SRID = 4326 };
                    }
                    else
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
                        apartment = _dbContext.Apartment.Add(new Apartment
                        {
                            UserId = userId,
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
                            ApartmentPreferences = preferences,
                            GeoLocation = new Point(request.Longitude, request.Latitude) { SRID = 4326 },
                            Status = "Available"
                        }).Entity;
                    }


                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home", new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("submit failed", ex);
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
