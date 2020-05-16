using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RoomateApp.Entities
{
    public partial class RoomateContext : DbContext
    {
        public RoomateContext()
        {
        }

        public RoomateContext(DbContextOptions<RoomateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<ApartmentPreferences> ApartmentPreferences { get; set; }
        public virtual DbSet<RoomDetails> RoomDetails { get; set; }
        public virtual DbSet<UserPreferences> UserPreferences { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=GUY-PC\\DEVEXPRESS;Database=sapishush;Trusted_Connection=True;", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.AdditionalComments)
                    .HasColumnName("Additional_Comments")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AvailableRooms).HasColumnName("Available_Rooms");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HasLift)
                    .HasColumnName("Has_Lift")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.HasLivingroom)
                    .HasColumnName("Has_Livingroom")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.HasParking)
                    .HasColumnName("Has_Parking")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.HouseholdPrice)
                    .HasColumnName("Household_Price")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.LeaseStartDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Modified).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Neighborhood).HasMaxLength(50);

                entity.Property(e => e.RoomsCount).HasColumnName("Rooms_Count");
                
                entity.Property(e => e.GeoLocation).HasColumnName("Geo_Location");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxPrice)
                    .HasColumnName("Tax_Price")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Apartment_User");
            });

            modelBuilder.Entity<ApartmentPreferences>(entity =>
            {
                entity.Property(e => e.AgePreferenceRate).HasColumnName("Age_Preference_Rate");

                entity.Property(e => e.ApartmentId).HasColumnName("Apartment_Id");

                entity.Property(e => e.CleanRate).HasColumnName("Clean_Rate");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FoodIssuesRate).HasColumnName("Food_Issues_Rate");

                entity.Property(e => e.KosherKitchenRate).HasColumnName("Kosher_Kitchen_Rate");

                entity.Property(e => e.Modified).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PetFriendlyRate).HasColumnName("Pet_Friendly_Rate");

                entity.Property(e => e.ReligiousRate).HasColumnName("Religious_Rate");

                entity.Property(e => e.SmokeRate).HasColumnName("Smoke_Rate");

                entity.Property(e => e.SocialFormatRate).HasColumnName("Social_Format_Rate");

                entity.HasOne(d => d.Apartment)
                    .WithOne(p => p.ApartmentPreferences)
                    .HasForeignKey<ApartmentPreferences>(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ApartmentPreferences_Apartment");
            });

            modelBuilder.Entity<RoomDetails>(entity =>
            {
                entity.ToTable("Room_Details");

                entity.Property(e => e.ApartmentId).HasColumnName("Apartment_Id");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Modified).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrivateBalcony)
                    .HasColumnName("Private_Balcony")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PrivateShower)
                    .HasColumnName("Private_Shower")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PrivateToilet)
                    .HasColumnName("Private_Toilet")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoomRent)
                    .HasColumnName("Room_Rent")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.RoomSize).HasColumnName("Room_Size");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StayedFurniture)
                    .HasColumnName("Stayed_Furniture")
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.RoomDetails)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_apid");
            });

            modelBuilder.Entity<UserPreferences>(entity =>
            {
                entity.Property(e => e.AgePreferenceRate).HasColumnName("Age_Preference_Rate");

                entity.Property(e => e.CleanRate).HasColumnName("Clean_Rate");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FoodIssuesRate).HasColumnName("Food_Issues_Rate");

                entity.Property(e => e.KosherKitchenRate).HasColumnName("Kosher_Kitchen_Rate");

                entity.Property(e => e.MaxPriceRange)
                    .HasColumnName("Max_Price_Range")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.MinPriceRange)
                    .HasColumnName("Min_Price_Range")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Modified).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PetFriendlyRate).HasColumnName("Pet_Friendly_Rate");

                entity.Property(e => e.ReligiousRate).HasColumnName("Religious_Rate");

                entity.Property(e => e.SmokeRate).HasColumnName("Smoke_Rate");

                entity.Property(e => e.SocialFormatRate).HasColumnName("Social_Format_Rate");
                
                entity.Property(e => e.GeoLocation).HasColumnName("Geo_Location");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPreferences)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Preferences_User");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(15);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(15);

                entity.Property(e => e.Modified).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
