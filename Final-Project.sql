DROP TABLE Room_Details
DROP TABLE ApartmentPreferences
DROP TABLE Apartment
DROP TABLE UserPreferences
DROP TABLE Users
GO

CREATE TABLE Users
(Id INT IDENTITY(1,1) PRIMARY KEY,
Cretead DATETIME2 DEFAULT GETDATE() NOT NULL,
Modified DATETIME2 DEFAULT GETDATE() NOT NULL,
Email NVARCHAR(50) NOT NULL,
Password NVARCHAR(15) NOT NULL,
First_Name NVARCHAR(15) NOT NULL,
Last_Name NVARCHAR(15) NOT NULL,
Phone_Number VARCHAR(10),
Gender VARCHAR(10) -- 'Male', 'Female', 'Other'
)

CREATE TABLE UserPreferences
(Id INT IDENTITY(1,1) PRIMARY KEY, --guy
Cretead DATETIME2 DEFAULT GETDATE() NOT NULL,
Modified DATETIME2 DEFAULT GETDATE() NOT NULL,
User_Id INT NOT NULL,
Smoke_Rate TINYINT NOT NULL,
Religious_Rate TINYINT NOT NULL,
Clean_Rate TINYINT NOT NULL,
Food_Issues_Rate TINYINT NOT NULL,
Social_Format_Rate TINYINT NOT NULL,
Kosher_Kitchen_Rate TINYINT NOT NULL,
Pet_Friendly_Rate TINYINT NOT NULL,
Age_Preference_Rate TINYINT NOT NULL,
Min_Price_Range NUMERIC(10),
Max_Price_Range NUMERIC(10),
Geo_Location GEOGRAPHY,
CONSTRAINT fk_Preferences_User FOREIGN KEY (User_Id)
				REFERENCES Users(Id)
				ON UPDATE CASCADE
				ON DELETE NO ACTION
)

CREATE TABLE Apartment
(Id INT IDENTITY(1,1) PRIMARY KEY,
Cretead DATETIME2 DEFAULT GETDATE() NOT NULL,
Modified DATETIME2 DEFAULT GETDATE() NOT NULL,
User_Id INT NOT NULL,
Status VARCHAR(20),
City NVARCHAR(50) NOT NULL,
Neighborhood NVARCHAR(50),
Street NVARCHAR(50) NOT NULL,
Number TINYINT,
Floor TINYINT,
LeaseStartDate DATETIME2 DEFAULT GETDATE() NOT NULL,
Has_Lift BIT DEFAULT 1,
Has_Parking BIT DEFAULT 1,
Rooms_Count TINYINT,
Available_Rooms TINYINT,
Has_Livingroom BIT DEFAULT 1,
Household_Price NUMERIC(8),
Tax_Price NUMERIC(8),
Additional_Comments VARCHAR(1000),
Geo_Location GEOGRAPHY,
CONSTRAINT fk_Apartment_User FOREIGN KEY (User_Id)
				REFERENCES Users(Id)
				ON UPDATE CASCADE
				ON DELETE NO ACTION
)

CREATE TABLE ApartmentPreferences
(Id INT IDENTITY(1,1) PRIMARY KEY,
Apartment_Id INT NOT NULL,
Cretead DATETIME2 DEFAULT GETDATE() NOT NULL,
Modified DATETIME2 DEFAULT GETDATE() NOT NULL,
Smoke_Rate TINYINT NOT NULL,
Religious_Rate TINYINT NOT NULL,
Clean_Rate TINYINT NOT NULL,
Food_Issues_Rate TINYINT NOT NULL,
Social_Format_Rate TINYINT NOT NULL,
Kosher_Kitchen_Rate TINYINT NOT NULL,
Pet_Friendly_Rate TINYINT NOT NULL,
Age_Preference_Rate TINYINT NOT NULL

CONSTRAINT fk_ApartmentPreferences_Apartment FOREIGN KEY (Apartment_Id)
				REFERENCES Apartment(Id)
				ON UPDATE CASCADE
				ON DELETE NO ACTION
)


CREATE TABLE Room_Details
(Id INT IDENTITY(1,1) PRIMARY KEY,
Cretead DATETIME2 DEFAULT GETDATE() NOT NULL,
Modified DATETIME2 DEFAULT GETDATE() NOT NULL,
Apartment_Id INT NOT NULL,
Status VARCHAR(20),
Room_Rent NUMERIC(9) NOT NULL,
Room_Size TINYINT,
Private_Toilet BIT DEFAULT 1,
Private_Shower BIT DEFAULT 1,
Private_Balcony BIT DEFAULT 1,
Stayed_Furniture NVARCHAR(1000),
CONSTRAINT fk_apid FOREIGN KEY (Apartment_Id)
				REFERENCES Apartment(Id)
				ON UPDATE CASCADE
				ON DELETE NO ACTION
)



---♫ inserts
--Users
insert into Users (Email, Password, First_Name, Last_Name, Phone_Number, Gender) values 
('Sap', '123456', 'Sapir', 'Cohen', '0505050505', 'Female'),
('Chen', '654321', 'Chen', 'Ovadia', NULL, 'Female'),
('Avi13', 'avi123', 'Avi', 'Malka', '036798643', 'Male'),
('Moshiko', '3e4r5t6y', 'Moshe' , 'Peretz',NULL, 'Male'),
('Batman', 'bru123', 'Bruce','wayne', '054534352', 'Male'),
('Kelly', '5TGB5B', 'Kelly', 'Janner', NULL, 'Female'),
('Peter', '123987', 'Peter', 'Bind', '0543534343', 'Male'),
('Dalia', '654pll', 'Dalia', 'Pesach', '0545673222', 'Other'),
('Asherk', 'ash65t5', 'Asher', 'Karet', '086326473', 'Male'),
('Nadlan Center', 'nadlan24', 'Tova' , 'Malki','058754923', 'Female'),
('Adirlev', 'ah5554', 'Adir','Lav', '0589756643', 'Male'),
('Shani', '17041996', 'Shani', 'Zahav', NULL, 'Femle'),
('Dani', '1d12d11', 'Dani', 'Cohen', '054555874', 'Male'),
('Liort', '654323', 'Lior', 'Tivob', '098765475', 'Male'),
('Niv5', 'my12niv', 'Niv', 'Frid', '0523453749', 'Male')


--UserPreferences
insert into UserPreferences (User_Id, Smoke_Rate, Religious_Rate,Clean_Rate,Food_Issues_Rate,Social_Format_Rate,Kosher_Kitchen_Rate,Pet_Friendly_Rate,Age_Preference_Rate,Min_Price_Range,Max_Price_Range,Geo_Location) values
(1, 3, 0, 5, 0, 1, 3, 1, 5, 3500, 4000, geography::Point(32.0853, 34.7818, 4326)),
(2, 1, 3, 5, 5, 0, 1, 5, 1, 2000, 6000, geography::Point(32.0853, 34.7818, 4326)),
(4, 1, 3, 5, 5, 0, 1, 5, 1, 1000, 2100, geography::Point(31.263157, 34.806034, 4326)),
(6, 1, 3, 5, 5, 0, 1, 5, 1, 2000, 6000, geography::Point(32.0853, 34.7818, 4326)),
(7, 1, 3, 5, 5, 0, 1, 5, 1, 2000, 6000, geography::Point(32.0853, 34.7818, 4326))


--Apartment
insert into Apartment (User_Id,Status,City,Neighborhood,Street,Number,Floor,LeaseStartDate,Has_Lift,Has_Parking,Rooms_Count,Available_Rooms,Has_Livingroom,Household_Price,Tax_Price,Additional_Comments, Geo_Location) values
(3, 'Available', 'Tel Aviv', 'Dafna', 'Shaul Hamelech', '2', '3', '2020-03-01', 0, 0, 3, 2, 1, 150, 700, 'A real pearl', geography::Point(32.078193, 34.792061, 4326)),
(9, 'Available', 'Tel Aviv', 'Weizman', 'Weizman', '2', '3', '2020-05-01', 1, 1, 1, 1, 1, 200, 950, 'high society place', geography::Point(32.085300, 34.781800, 4326)),
(5, 'Available', 'Tel Aviv', 'Florentin', 'Florentin', '32', '1', '2020-06-01', 0, 0, 2, 2, 0, 50, 400, 'Old building', geography::Point(32.056298, 34.770320, 4326)),
(8, 'Available', 'Tel Aviv', 'Lamed', 'Meir Fainshtein', '36', '4', '2020-07-01', 1, 1, 3, 1, 1, 200, 600, 'Great for students', geography::Point(32.127986, 34.801477, 4326)),
(8, 'Available', 'Ramat Gan', 'Krinitzy', 'Mendes', '12', '0', '2020-06-21', 0, 1, 3, 1, 0, 200, 600, 'For musicians', geography::Point(32.052286, 34.843103, 4326)),
(13, 'Available', 'Tel Aviv', 'Merkaz', 'Yona Hanavi', '30', '2', '2020-07-15', 0, 2, 3, 0, 1, 140, 500, 'באוהאוס הורס', geography::Point(32.072174, 34.767810, 4326)),
(11, 'Available', 'Ramat Gan', 'Bialik', 'Harishonim', '3', '15', '2020-06-12', 1, 1, 3, 1, 1, 800, 600, 'Prime Location', geography::Point(32.082234, 34.803774, 4326)),
(10, 'Available', 'Tel Aviv', 'Lev Tel Aviv', 'Rothshild Bvd', '15', '15', '2020-06-12', 1, 1, 3, 1, 1, 800, 600, 'Prime Location', geography::Point(32.063185, 34.770938, 4326)),
(15, 'Available', 'Tel Aviv', 'South', 'Har Zion', '1', '1', '2020-06-30', 0, 0, 2, 0, 1, 70, 400, 'בשכונה מתפתחת עם שכנים נהדרים', geography::Point(32.059467, 34.778234, 4326)),
(5, 'Available', 'Ramat Gan', NULL, 'Aluf David', '123', '1', '2020-07-30', 1, 1, 2, 1, 1, 250, 500, 'young vibe', geography::Point(32.057025, 34.825461, 4326)),
(14, 'Available', 'Beer Sheva', 'Gimel', 'Golomb', '6', '1', '2020-08-01', 0, 0, 4, 2, 1, 80, 190, 'For clean students only', geography::Point(31.253151, 34.802239, 4326)),
(12, 'Available', 'Beer Sheva', 'Dalet', 'Felix', '38', '2', '2020-07-15', 0, 0, 4, 2, 1, 100, 120, 'Chill vibes with smoke area', geography::Point(31.274766, 34.801883, 4326)),
(12, 'Available', 'Beer Sheva', 'Dalet', 'Hamada', '37', '1', '2020-07-10', 0, 0, 4, 2, 1, 120, 250, 'We love weekend parties!', geography::Point(31.273204, 34.806248, 4326))

--ApartmentPreferences
insert into ApartmentPreferences (Apartment_Id,Smoke_Rate,Religious_Rate,Clean_Rate,Food_Issues_Rate,Social_Format_Rate,Kosher_Kitchen_Rate,Pet_Friendly_Rate,Age_Preference_Rate) values 
(1, 0, 5, 3, 0, 1, 4, 3, 5),
(2, 1, 3, 1, 0, 0, 1, 1, 5),
(3, 0, 5, 1, 3, 3, 0, 1, 0),
(4, 0, 5, 1, 3, 3, 0, 1, 0),
(5, 5, 5, 1, 3, 5, 0, 1, 5),
(6, 0, 5, 1, 3, 3, 0, 1, 1),
(7, 0, 5, 1, 3, 3, 3, 1, 5),
(8, 0, 5, 1, 5, 3, 5, 1, 3),
(9, 0, 5, 1, 3, 3, 1, 1, 0),
(10, 0, 5, 1, 3, 3, 0, 1, 5),
(11, 1, 3, 3, 5, 0, 0, 1, 1),
(12, 0, 5, 3, 3, 5, 0, 3, 2),
(13, 0,1, 1, 3, 1, 0, 1, 5)

--Room_Details
insert into Room_Details (Apartment_Id,Status,Room_Rent,Room_Size,Private_Toilet,Private_Shower,Private_Balcony,Stayed_Furniture) values 
(1, 'Available', 4000, 16, 1, 1, 0, 'Kitchen table, TV, Smelly underwear'),
(2, 'Available', 6000, 9, 1, 1, 1, 'Bed., closet and stuff'),
(3, 'Available', 2000, 20, 0, 0, 0, 'Hamutzim'),
(4, 'Available', 2100, 17, 1, 0, 0, 'Bed and closet'),
(5, 'Available', 2800, 20, 0, 0, 0, 'All in. just bring clothes'),
(6, 'Available', 2750, 15, 0, 0, 1, 'Nothing'),
(7, 'Available', 4000, 30, 1, 1, 1, '1 bed,1 TV and a light ball'),
(8, 'Available', 2500, 25, 1, 0, 0, '1 big bed'),
(9, 'Available', 3700, 28, 1, 1, 0, '1 bed,1 TV and a light ball'),
(10, 'Available', 1300, 28, 0, 1, 0, '1 bed,1 TV and a light ball'),
(11, 'Available', 2000, 28, 1, 0, 0, '1 bed,1 TV and a light ball'),
(12, 'Available', 2600, 28, 0, 1, 0, '1 bed,1 TV and a light ball'),
(13, 'Available', 3700, 28, 1, 0, 0, '1 bed,1 TV and a light ball')
