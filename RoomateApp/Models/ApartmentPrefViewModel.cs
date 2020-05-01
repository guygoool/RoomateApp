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
    }
}
