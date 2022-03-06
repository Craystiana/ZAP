USE [ZAP]
GO
DELETE FROM [dbo].[Car]
ALTER TABLE [dbo].[Car] ADD Name nvarchar(max) NOT NULL
GO