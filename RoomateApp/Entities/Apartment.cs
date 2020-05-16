using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace RoomateApp.Entities
{
    public partial class Apartment
    {
        public Apartment()
        {
            RoomDetails = new HashSet<RoomDetails>();
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public byte? Number { get; set; }
        public byte? Floor { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public bool? HasLift { get; set; }
        public bool? HasParking { get; set; }
        public byte? RoomsCount { get; set; }
        public byte? AvailableRooms { get; set; }
        public bool? HasLivingroom { get; set; }
        public decimal? HouseholdPrice { get; set; }
        public decimal? TaxPrice { get; set; }
        public string AdditionalComments { get; set; }
        public Point GeoLocation { get; set; }

        public virtual Users User { get; set; }
        public virtual ApartmentPreferences ApartmentPreferences { get; set; }
        public virtual ICollection<RoomDetails> RoomDetails { get; set; }
    }
}
