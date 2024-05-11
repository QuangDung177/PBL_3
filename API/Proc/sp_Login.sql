USE [PBL3_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_Login]    Script Date: 02/05/2024 10:21:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Login] 
    @username NVARCHAR(50),
    @password NVARCHAR(50)
AS
BEGIN
    DECLARE @count INT;
    SELECT @count = COUNT(*)
    FROM Account
    WHERE Username = @username AND [password] = @password;
    IF @count > 0
    BEGIN
        SELECT N'Đăng nhập thành công' AS Result,
               C.customerID,
               C.fullName,
               C.birthDate,
               C.gender,
               C.address,
               C.phoneNumber,
               C.accountStatus
        FROM Account A
        INNER JOIN Customer C ON A.accountID = C.accountID
        WHERE A.Username = @username AND A.[password] = @password;
    END
    ELSE
        SELECT N'Đăng nhập thất bại' AS Result;
END;
