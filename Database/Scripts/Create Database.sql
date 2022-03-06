CREATE TABLE [dbo].[CarBrand](
	[CarBrandId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](max) NULL,
 CONSTRAINT [PK_CarBrand] PRIMARY KEY CLUSTERED 
(
	[CarBrandId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




CREATE TABLE [dbo].[CarClass](
	[CarClassId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](max) NULL,
 CONSTRAINT [PK_CarClass] PRIMARY KEY CLUSTERED 
(
	[CarClassId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




CREATE TABLE [dbo].[CarType](
	[CarTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](max) NULL,
 CONSTRAINT [PK_CarType] PRIMARY KEY CLUSTERED 
(
	[CarTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




CREATE TABLE [dbo].[Car](
	[CarId] [int] NOT NULL IDENTITY(1, 1),
	[CarClassId] [int] NOT NULL,
	[CarTypeId] [int] NOT NULL,
	[CarBrandId] [int] NOT NULL,
	[LicensePlate] [nvarchar](50) NOT NULL,
	[Odometer] [int] NOT NULL,
	[ManufactureDate] [date] NOT NULL,
	[Price] [int] NOT NULL,
	[Picture] [varbinary](max) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_CarBrand] FOREIGN KEY([CarBrandId])
REFERENCES [dbo].[CarBrand] ([CarBrandId])
GO

ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_CarBrand]
GO

ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_CarClass] FOREIGN KEY([CarClassId])
REFERENCES [dbo].[CarClass] ([CarClassId])
GO

ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_CarClass]
GO

ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_CarType] FOREIGN KEY([CarTypeId])
REFERENCES [dbo].[CarType] ([CarTypeId])
GO

ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_CarType]
GO




CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL IDENTITY(1, 1),
	[UserRoleId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[Salt] [nvarchar](max) NOT NULL,
	[IsBlacklisted] [bit] DEFAULT 0 NOT NULL, 
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRole] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([UserRoleId])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRole]
GO




CREATE TABLE [dbo].[ReservationType](
	[ReservationTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ReservationType] PRIMARY KEY CLUSTERED 
(
	[ReservationTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[Reservation](
	[ReservationId] [int] NOT NULL IDENTITY(1, 1),
	[UserId] [int] NOT NULL,
	[CarId] [int] NOT NULL,
	[ReservationTypeId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Incident] [bit] DEFAULT 0 NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([CarId])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Car]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_ReservationType] FOREIGN KEY([ReservationTypeId])
REFERENCES [dbo].[ReservationType] ([ReservationTypeId])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_ReservationType]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_User]
GO