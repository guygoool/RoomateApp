using System;
using System.Collections.Generic;

namespace RoomateApp.Entities
{
    public partial class RoomDetails
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int ApartmentId { get; set; }
        public string Status { get; set; }
        public decimal RoomRent { get; set; }
        public byte? RoomSize { get; set; }
        public bool? PrivateToilet { get; set; }
        public bool? PrivateShower { get; set; }
        public bool? PrivateBalcony { get; set; }
        public string StayedFurniture { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
