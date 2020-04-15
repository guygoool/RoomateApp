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
        [Required]
        public string City { get; set; }
        public string Neighberhood { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string Floor { get; set; }
        [Required]
        public DateTime LeaseStartDate { get; set; }
        [Required]
        public bool HasLift { get; set; }
        [Required]
        public bool HasParking { get; set; }
        [Required]
        public int RoomsCount { get; set; }
        [Required]
        public int AvailableRoomsCount { get; set; }
        [Required]
        public bool HasLivingroom { get; set; }
        [Required]
        public int HouseholdPrice { get; set; }
        [Required]
        public int TaxPrice { get; set; }
        [Required]
        public string AdditionalComments { get; set; }
    }
}
