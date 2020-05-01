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
        public decimal RoomRentPrice { get; set; }
        [Required]
        public byte RoomRentSize { get; set; }
        [Required]
        public bool IsPrivateShower { get; set; }
        [Required]
        public bool IsPrivateToilet { get; set; }
        [Required]
        public bool IsPrivateBalcony { get; set; }
        public string StayedFurniture { get; set; }
    }
}
