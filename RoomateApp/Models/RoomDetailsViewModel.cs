using RoomateApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Models
{
    public class RoomDetailsViewModel
    {
        [Required]
        [Display(Name = "עלות השכרה")]
        public decimal RoomRentPrice { get; set; }
        [Required]
        [Display(Name = "גודל חדר")]
        public byte RoomRentSize { get; set; }
        [Required]
        [Display(Name = "האם יש מקלחת פרטית?")]
        public bool IsPrivateShower { get; set; }
        [Required]
        [Display(Name = "האם יש שירותים פרטיים?")]
        public bool IsPrivateToilet { get; set; }
        [Required]
        [Display(Name = "האם יש מרפסת פרטית?")]
        public bool IsPrivateBalcony { get; set; }
        [Display(Name = "ריהוט שנשאר")]
        public string StayedFurniture { get; set; }
    }

    public static class RoomDetailsExtensions
    {
        public static RoomDetailsViewModel ToViewModel(this RoomDetails roomDetails)
        {
            return roomDetails == null ? null : new RoomDetailsViewModel
            {
                IsPrivateBalcony = roomDetails.PrivateBalcony.GetValueOrDefault(false),
                IsPrivateShower = roomDetails.PrivateShower.GetValueOrDefault(false),
                IsPrivateToilet = roomDetails.PrivateToilet.GetValueOrDefault(false),
                RoomRentPrice = roomDetails.RoomRent,
                RoomRentSize = roomDetails.RoomSize.GetValueOrDefault(0),
                StayedFurniture = roomDetails.StayedFurniture,
            };
        }
    }
}
