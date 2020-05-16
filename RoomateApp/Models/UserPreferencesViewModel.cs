using RoomateApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Models
{
    public class UserPreferencesViewModel
    {
        public int UserId { get; set; }
        [Required]
        public byte SmokeRate { get; set; }
        [Required]
        public byte ReligiousRate { get; set; }
        [Required]
        public byte CleanRate { get; set; }
        [Required]
        public byte FoodIssuesRate { get; set; }
        [Required]
        public byte SocialFormatRate { get; set; }
        [Required]
        public byte KosherKitchenRate { get; set; }
        [Required]
        public byte PetFriendlyRate { get; set; }
        [Required]
        public byte AgePreferenceRate { get; set; }
        [Required]
        public decimal? MinPriceRange { get; set; }
        [Required]
        public decimal? MaxPriceRange { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public static class UserPrefExtensions
    {
        public static UserPreferencesViewModel ToViewModel(this UserPreferences pref)
        {
            return new UserPreferencesViewModel
            {
                UserId = pref.UserId,
                AgePreferenceRate = pref.AgePreferenceRate,
                CleanRate = pref.CleanRate,
                FoodIssuesRate = pref.FoodIssuesRate,
                KosherKitchenRate = pref.KosherKitchenRate,
                MaxPriceRange = pref.MaxPriceRange,
                MinPriceRange = pref.MinPriceRange,
                PetFriendlyRate = pref.PetFriendlyRate,
                ReligiousRate = pref.ReligiousRate,
                SmokeRate = pref.SmokeRate,
                SocialFormatRate = pref.SocialFormatRate
            };
        }
    }
}
