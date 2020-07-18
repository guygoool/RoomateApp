using RoomateApp.Entities;
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
        [Display(Name = "האם חשוב לך שלא יעשנו בדירה?")]
        public byte SmokeRate { get; set; }
        [Required]
        [Display(Name = "האם חשוב לך שהבית ישמור שבת?")]
        public byte ReligiousRate { get; set; }
        [Required]
        [Display(Name = "האם הרגלי ניקיון קבועים חשובים לך?")]
        public byte CleanRate { get; set; }
        [Required]
        [Display(Name = "האם חשוב לך שהבית יהיה צמחוני?")]
        public byte FoodIssuesRate { get; set; }
        [Required]
        [Display(Name = "האם חשוב לך שיהיה ניתן לארח אנשים בדירה באופן קבוע?")]
        public byte SocialFormatRate { get; set; }
        [Required]
        [Display(Name = "האם חשוב לך שהמטבח יהיה כשר?")]
        public byte KosherKitchenRate { get; set; }
        [Required]
        [Display(Name = "האם חשוב לך שניתן יהיה להכניס בע\"ח לדירה ? ")]
        public byte PetFriendlyRate { get; set; }
        [Required]
        [Display(Name = "האם חשוב לך שהשותפים יהיו מעל גיל 26?")]
        public byte AgePreferenceRate { get; set; }
    }

    public static class ApartmentPrefExtensions
    {
        public static ApartmentPrefViewModel ToViewModel(this ApartmentPreferences pref)
        {
            return pref == null ? new ApartmentPrefViewModel() : new ApartmentPrefViewModel
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
