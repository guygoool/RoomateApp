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
        public string City { get; set; }
        public string Neighberhood { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        [Required]
        public byte Floor { get; set; }
        [Required]
        [Display(Name = "תאריך תחילת חוזה")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseStartDate { get; set; }
        public bool HasLift { get; set; }
        public bool HasParking { get; set; }
        [Required]
        public byte RoomsCount { get; set; }
        [Required]
        public byte AvailableRoomsCount { get; set; }
        public bool HasLivingroom { get; set; }
        public int HouseholdPrice { get; set; }
        public int TaxPrice { get; set; }
        public string AdditionalComments { get; set; }
        public RoomDetailsViewModel RoomDetails { get; set; }
        public ApartmentPrefViewModel Preferences { get; set; }
    }
}
