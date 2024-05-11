USE [PBL3_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_Reg]    Script Date: 02/05/2024 10:21:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Reg]
    @accountID INT,
    @userName VARCHAR(50),
    @password VARCHAR(50),
    @email VARCHAR(50),
    @customerID INT,
    @fullName NVARCHAR(50),
    @birthDate DATE,
    @gender BIT,
    @address NVARCHAR(70),
    @phoneNumber VARCHAR(15),
    @accountStatus NVARCHAR(30)
AS
BEGIN

    INSERT INTO Account (accountID, userName, [password], [email])
    VALUES (@accountID, @userName, @password, @email)

    INSERT INTO Customer (customerID, accountID, fullName, birthDate, gender, [address], phoneNumber, accountStatus)
    VALUES (@customerID, @accountID, @fullName, @birthDate, @gender, @address, @phoneNumber, @accountStatus)
END
