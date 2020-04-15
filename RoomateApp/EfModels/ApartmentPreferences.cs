using System;
using System.Collections.Generic;

namespace RoomateApp.EfModels
{
    public partial class ApartmentPreferences
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public DateTime Cretead { get; set; }
        public DateTime Modified { get; set; }
        public byte SmokeRate { get; set; }
        public byte ReligiousRate { get; set; }
        public byte CleanRate { get; set; }
        public byte FoodIssuesRate { get; set; }
        public byte SocialFormatRate { get; set; }
        public byte KosherKitchenRate { get; set; }
        public byte PetFriendlyRate { get; set; }
        public byte AgePreferenceRate { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
