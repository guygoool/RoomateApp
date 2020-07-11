using RoomateApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Models
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string City { get; set; }
        public string Neighberhood { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        [Required]
        [Display(Name = "קומה")]
        public byte Floor { get; set; }
        [Required]
        [Display(Name = "תאריך תחילת חוזה")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LeaseStartDate { get; set; }
        [Display(Name = "מעלית")]
        public bool HasLift { get; set; }
        [Display(Name = "חניה")]
        public bool HasParking { get; set; }
        [Required]
        [Display(Name = "מס' חדרים")]
        public byte RoomsCount { get; set; }
        [Required]
        [Display(Name = "מס' חדרים פנויים")]
        public byte AvailableRoomsCount { get; set; }
        [Display(Name = "סלון")]
        public bool HasLivingroom { get; set; }
        [Display(Name = "ועד הבית")]
        public int HouseholdPrice { get; set; }
        [Display(Name = "ארנונה")]
        public int TaxPrice { get; set; }
        [Display(Name = "הערות נוספות")]
        public string AdditionalComments { get; set; }
        [Display(Name = "ארנונה")]
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public RoomDetailsViewModel RoomDetails { get; set; }
        public ApartmentPrefViewModel Preferences { get; set; }
    }

    public static class ApartmentExtensions
    {
        public static ApartmentViewModel ToViewModel(this Apartment apartment)
        {
            return apartment == null ? null : new ApartmentViewModel
            {
                UserId = apartment.UserId,
                AdditionalComments = apartment.AdditionalComments,
                AvailableRoomsCount = apartment.AvailableRooms.GetValueOrDefault(1),
                City = apartment.City,
                Floor = apartment.Floor.GetValueOrDefault(0),
                HasLift = apartment.HasLift.GetValueOrDefault(false),
                HasLivingroom = apartment.HasLivingroom.GetValueOrDefault(false),
                HasParking = apartment.HasParking.GetValueOrDefault(false),
                HouseholdPrice = (int)apartment.HouseholdPrice.GetValueOrDefault(0),
                HouseNumber = apartment.Number.GetValueOrDefault(0).ToString(),
                Id = apartment.Id,
                Latitude = apartment.GeoLocation.Y,
                Longitude = apartment.GeoLocation.X,
                LeaseStartDate = apartment.LeaseStartDate,
                Neighberhood = apartment.Neighborhood,
                Preferences = apartment.ApartmentPreferences.ToViewModel(),
                RoomDetails = apartment.RoomDetails.FirstOrDefault()?.ToViewModel(),
                RoomsCount = apartment.RoomsCount.GetValueOrDefault(0),
                Street = apartment.Street,
                TaxPrice = (int)apartment.TaxPrice.GetValueOrDefault(0)
            };
        }
    }
}
