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

        [HttpGet]
        //[Route("{id:int}")]
        public IActionResult Index(int id)
        {
            var apartment = _dbContext.Apartment
                .Include(c => c.ApartmentPreferences)
                .Include(c => c.RoomDetails)
                .FirstOrDefault(a => a.Id == id);

            return View(apartment.ToViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(ApartmentViewModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingApartment = await _dbContext.Apartment.FirstOrDefaultAsync(c => c.Id == request.Id);
                    if (existingApartment != null)
                    {
                        var roomDetails = existingApartment.RoomDetails.FirstOrDefault();
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
                            existingApartment.RoomDetails.Add(new RoomDetails
                            {
                                ApartmentId = existingApartment.Id,
                                PrivateBalcony = request.RoomDetails.IsPrivateBalcony,
                                PrivateShower = request.RoomDetails.IsPrivateShower,
                                PrivateToilet = request.RoomDetails.IsPrivateToilet,
                                RoomRent = request.RoomDetails.RoomRentPrice,
                                RoomSize = request.RoomDetails.RoomRentSize,
                                Status = "Available",
                                StayedFurniture = request.RoomDetails.StayedFurniture
                            });
                        }

                        var preferences = existingApartment.ApartmentPreferences;
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
                            existingApartment.ApartmentPreferences = new ApartmentPreferences
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

                        existingApartment.UserId = request.UserId;
                        existingApartment.AdditionalComments = request.AdditionalComments;
                        existingApartment.ApartmentPreferences = preferences;
                        existingApartment.AvailableRooms = request.AvailableRoomsCount;
                        existingApartment.Floor = request.Floor;
                        existingApartment.HasLift = request.HasLift;
                        existingApartment.HasLivingroom = request.HasLivingroom;
                        existingApartment.HasParking = request.HasParking;
                        existingApartment.HouseholdPrice = request.HouseholdPrice;
                        existingApartment.LeaseStartDate = request.LeaseStartDate;
                        existingApartment.RoomDetails = new List<RoomDetails> { roomDetails };
                        existingApartment.RoomsCount = request.RoomsCount;
                        existingApartment.Status = "Available";
                        existingApartment.TaxPrice = request.TaxPrice;
                        existingApartment.GeoLocation = new Point(request.Longitude, request.Latitude) { SRID = 4326 };
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
                        _dbContext.Apartment.Add(new Apartment
                        {
                            UserId = request.UserId,
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
                        });
                    }


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
