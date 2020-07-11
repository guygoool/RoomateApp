using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Entities
{
    public class SP_FindMatch
    {
        public int ApartmentId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public decimal TotalRating { get; set; }
        public decimal SmokeRating { get; set; }
        public decimal ReligiousRating{ get; set; }
        public decimal CleanRating{ get; set; }
        public decimal FoodIssuesRating{ get; set; }
        public decimal SocialFormatRating{ get; set; }
        public decimal KosherKitchenRating{ get; set; }
        public decimal PetFriendlyRating{ get; set; }
        public decimal AgePreferenceRating { get; set; }
        public int DistanceRating { get; set; }
        public int PriceRating { get; set; }

    }
}
