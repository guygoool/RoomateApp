using System;
using System.Collections.Generic;

namespace RoomateApp.EfModels
{
    public partial class Users
    {
        public Users()
        {
            Apartment = new HashSet<Apartment>();
            UserPreferences = new HashSet<UserPreferences>();
        }

        public int Id { get; set; }
        public DateTime Cretead { get; set; }
        public DateTime Modified { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<Apartment> Apartment { get; set; }
        public virtual ICollection<UserPreferences> UserPreferences { get; set; }
    }
}
