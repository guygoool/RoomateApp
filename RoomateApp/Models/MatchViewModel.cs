using RoomateApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Models
{
    public class MatchViewModel
    {
        public List<MatchResultViewModel> Matches { get; set; }
    }

    public class MatchResultViewModel
    {
        public int ApartmentId { get; set; }
        public string ApartmentOwnerFullName { get; set; }
        public string ApartmentOwnerPhoneNumber { get; set; }
        public decimal TotalRating { get; set; }
        public decimal SmokeRating { get; set; }
        public decimal ReligiousRating { get; set; }
        public decimal CleanRating { get; set; }
        public decimal FoodIssuesRating { get; set; }
        public decimal SocialFormatRating { get; set; }
        public decimal KosherKitchenRating { get; set; }
        public decimal PetFriendlyRating { get; set; }
        public decimal AgePreferenceRating { get; set; }
        public int DistanceRating { get; set; }
        public int PriceRating { get; set; }
    }

    public static class MatchExtensions
    {
        public static MatchResultViewModel ToViewModel(this SP_FindMatch match)
        {
            return match == null ? null : new MatchResultViewModel
            {
                ApartmentId = match.ApartmentId,
                ApartmentOwnerFullName = $"{match.OwnerFirstName} {match.OwnerLastName}",
                ApartmentOwnerPhoneNumber = match.OwnerPhoneNumber,
                TotalRating = match.TotalRating,
                AgePreferenceRating = match.AgePreferenceRating,
                CleanRating = match.CleanRating,
                PriceRating = match.PriceRating,
                DistanceRating = match.DistanceRating,
                PetFriendlyRating = match.PetFriendlyRating,
                KosherKitchenRating = match.KosherKitchenRating,
                FoodIssuesRating = match.FoodIssuesRating,
                ReligiousRating = match.ReligiousRating,
                SmokeRating = match.SmokeRating,
                SocialFormatRating = match.SocialFormatRating
            };
        }
    }
}
