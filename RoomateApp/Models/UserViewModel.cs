using RoomateApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomateApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public static class UserExtensions
    {
        public static UserViewModel ToViewModel(this Users user)
        {
            Enum.TryParse(user.Gender, out Gender gender);
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = gender,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                UserLogin = user.UserLogin
            };
        }
    }
}
