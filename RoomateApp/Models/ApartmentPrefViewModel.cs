﻿using RoomateApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Models
{
    public class ApartmentPrefViewModel
    {
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte SmokeRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte ReligiousRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte CleanRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte FoodIssuesRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte SocialFormatRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte KosherKitchenRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte PetFriendlyRate { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "{0} has to be between {1} and {2} characters.")]
        public byte AgePreferenceRate { get; set; }
    }

    public static class ApartmentPrefExtensions
    {
        public static ApartmentPrefViewModel ToViewModel(this ApartmentPreferences pref)
        {
            return pref == null ? null : new ApartmentPrefViewModel
            {
                AgePreferenceRate = pref.AgePreferenceRate,
                CleanRate = pref.CleanRate,
                FoodIssuesRate = pref.FoodIssuesRate,
                KosherKitchenRate = pref.KosherKitchenRate,
                PetFriendlyRate = pref.PetFriendlyRate,
                ReligiousRate = pref.ReligiousRate,
                SmokeRate = pref.SmokeRate,
                SocialFormatRate = pref.SocialFormatRate
            };
        }
    }
}
