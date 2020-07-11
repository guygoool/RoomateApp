/****** Object:  StoredProcedure [dbo].[FindMatch]    Script Date: 11/07/2020 13:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[FindMatch]
	@userId int = 2
AS
BEGIN
	SET NOCOUNT ON;

;with userPref as (
	select * from UserPreferences where User_Id = @userId
),
ratings as (
	select 
		Apartment.Id,
		case 
			when ABS(cast(ApartmentPreferences.Smoke_Rate as decimal) - cast((select top 1 Smoke_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Smoke_Rate as decimal) - cast((select top 1 Smoke_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Smoke_Rate as decimal) - cast((select top 1 Smoke_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as smokeRating,
		case 
			when ABS(cast(ApartmentPreferences.Religious_Rate as decimal) - cast((select top 1 Religious_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Religious_Rate as decimal) - cast((select top 1 Religious_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Religious_Rate as decimal) - cast((select top 1 Religious_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as ReligiousRating,
		case 
			when ABS(cast(ApartmentPreferences.Clean_Rate as decimal) - cast((select top 1 Clean_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Clean_Rate as decimal) - cast((select top 1 Clean_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Clean_Rate as decimal) - cast((select top 1 Clean_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as CleanRating,
		case 
			when ABS(cast(ApartmentPreferences.Food_Issues_Rate as decimal) - cast((select top 1 Food_Issues_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Food_Issues_Rate as decimal) - cast((select top 1 Food_Issues_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Food_Issues_Rate as decimal) - cast((select top 1 Food_Issues_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as FoodIssuesRating,
		case 
			when ABS(cast(ApartmentPreferences.Social_Format_Rate as decimal) - cast((select top 1 Social_Format_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Social_Format_Rate as decimal) - cast((select top 1 Social_Format_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Social_Format_Rate as decimal) - cast((select top 1 Social_Format_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as SocialFormatRating,
		case 
			when ABS(cast(ApartmentPreferences.Kosher_Kitchen_Rate as decimal) - cast((select top 1 Kosher_Kitchen_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Kosher_Kitchen_Rate as decimal) - cast((select top 1 Kosher_Kitchen_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Kosher_Kitchen_Rate as decimal) - cast((select top 1 Kosher_Kitchen_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as KosherKitchenRating,
		case 
			when ABS(cast(ApartmentPreferences.Pet_Friendly_Rate as decimal) - cast((select top 1 Pet_Friendly_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Pet_Friendly_Rate as decimal) - cast((select top 1 Pet_Friendly_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Pet_Friendly_Rate as decimal) - cast((select top 1 Pet_Friendly_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as PetFriendlyRating,
		case 
			when ABS(cast(ApartmentPreferences.Age_Preference_Rate as decimal) - cast((select top 1 Age_Preference_Rate from userPref) as decimal)) = 0 then 7.5
			when ABS(cast(ApartmentPreferences.Age_Preference_Rate as decimal) - cast((select top 1 Age_Preference_Rate from userPref) as decimal)) in (1, 2) then 5
			when ABS(cast(ApartmentPreferences.Age_Preference_Rate as decimal) - cast((select top 1 Age_Preference_Rate from userPref) as decimal)) in (3, 4) then 2
			else 0
		end as AgePreferenceRating,
		case 
			when (select top 1 Geo_Location from userPref).STDistance(Geo_Location) < 2000 then 20
			when (select top 1 Geo_Location from userPref).STDistance(Geo_Location) < 4000 then 12
			when (select top 1 Geo_Location from userPref).STDistance(Geo_Location) < 6000 then 6
			else 0
		end as DistanceRating,
		case 
			when Room_Details.Room_Rent <= (select top 1 Min_Price_Range from userPref) + (((select top 1 Max_Price_Range from userPref) - (select top 1 Min_Price_Range from userPref))/3 + 1) then 20
			when Room_Details.Room_Rent <= (select top 1 Min_Price_Range from userPref) + 2*(((select top 1 Max_Price_Range from userPref) - (select top 1 Min_Price_Range from userPref))/3 + 1) then 12
			when Room_Details.Room_Rent <= (select top 1 Min_Price_Range from userPref) + 3*(((select top 1 Max_Price_Range from userPref) - (select top 1 Min_Price_Range from userPref))/3 + 1) then 6
			else 0
		end as priceRating
	from Apartment
	inner join Room_Details on Room_Details.Apartment_Id = Apartment.Id
	inner join ApartmentPreferences on ApartmentPreferences.Apartment_Id = Apartment.Id
	where (select top 1 Geo_Location from userPref).STDistance(Geo_Location) < 7000
	and Room_Details.Room_Rent between (select top 1 Min_Price_Range from userPref) and (select top 1 Max_Price_Range from userPref)
)
select  
	Apartment.Id as ApartmentId,
	Users.First_Name as OwnerFirstName,
	Users.Last_Name as OwnerLastName,
	Users.Phone_Number as OwnerPhoneNumber,
	(
		ratings.smokeRating + 
		ratings.ReligiousRating + 
		ratings.CleanRating + 
		ratings.FoodIssuesRating + 
		ratings.SocialFormatRating + 
		ratings.KosherKitchenRating + 
		ratings.PetFriendlyRating + 
		ratings.AgePreferenceRating + 
		ratings.DistanceRating + 
		ratings.priceRating
	) as TotalRating,
	ratings.smokeRating,
	ratings.ReligiousRating,
	ratings.CleanRating,
	ratings.FoodIssuesRating,
	ratings.SocialFormatRating,
	ratings.KosherKitchenRating,
	ratings.PetFriendlyRating,
	ratings.AgePreferenceRating,
	ratings.DistanceRating,
	ratings.priceRating
from ratings
join Apartment on Apartment.Id = ratings.Id
join Users on Users.Id = Apartment.User_Id
order by TotalRating desc

END