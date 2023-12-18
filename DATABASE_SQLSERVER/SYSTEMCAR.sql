CREATE DATABASE SYSTEMCAR
GO

USE SYSTEMCAR
GO

CREATE TABLE ACCOUNT
(
	id INT IDENTITY PRIMARY KEY,
	fullname NVARCHAR(100) NOT NULL,
	username NVARCHAR(100) NOT NULL,
	password NVARCHAR(100) NOT NULL DEFAULT 0,
	role NVARCHAR(100) NOT NULL,
)
GO

CREATE TABLE CUSTOMER
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	fullname NVARCHAR(100) NOT NULL,
	address NVARCHAR(100) NOT NULL DEFAULT N'TP Hồ Chí Minh',
	mobile int NOT NULL DEFAULT 0938383838,
)
GO

CREATE TABLE SCHEDULE
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Cho thuê
)
GO

CREATE TABLE CATEGORY
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	brand NVARCHAR(100) NOT NULL,
	price FLOAT NOT NULL DEFAULT 0,
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	idCustomer INT NOT NULL,
	idCategory INT NOT NULL,
	type NVARCHAR(100) NOT NULL,
	feature NVARCHAR(100) NOT NULL,
	fuel NVARCHAR(100) NOT NULL,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	status INT NOT NULL DEFAULT 0 -- (1: đã thanh toán) && (0: chưa thanh toán)
	
	FOREIGN KEY (idCustomer) REFERENCES dbo.Customer(id),
	FOREIGN KEY (idCategory) REFERENCES dbo.Category(id)
)
GO


----ADD data
--Acount
INSERT INTO DBO.ACCOUNT (FULLNAME, USERNAME, PASSWORD, ROLE)
VALUES (N'Min', N'admin', N'admin', N'admin')
GO

INSERT INTO DBO.ACCOUNT (FULLNAME, USERNAME, PASSWORD, ROLE)
VALUES (N'Minh Hy', N'1', N'1', N'admin')
GO

INSERT INTO DBO.ACCOUNT (FULLNAME, USERNAME, PASSWORD, ROLE)
VALUES (N'Biii', N'a', N'a', N'staff')
GO

INSERT INTO DBO.ACCOUNT (FULLNAME, USERNAME, PASSWORD, ROLE)
VALUES (N'Min', N'min', N'min', N'staff')
GO
---SCHEDELE
INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 4 chỗ (Mini)', N'Đang cho thuê')
GO

INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 4 chỗ (Sedan)', N'Trống')
GO

INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 4 chỗ (Hatchback)', N'Trống')
GO

INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 5 chỗ (CUV Gấm cao)', N'Trống')
GO

INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 7 chỗ (SUV Gấm cao)', N'Đang cho thuê')
GO

INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 7 chỗ (MPV Gấm thấp)', N'Đang cho thuê')
GO

INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Bán tải', N'Đang cho thuê')
GO
INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 19 chỗ', N'Trống')
GO
INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 23 chỗ (Sedan)', N'Trống')
GO
INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 25 chỗ', N'Trống')
GO
INSERT INTO DBO.SCHEDULE(name, status)
VALUES (N'Xe 31 chỗ (CUV Gấm cao)', N'Trống')
GO


--Category
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 4 chỗ (Mini)', N'Ford', 123000)
GO
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 4 chỗ (Sedan)', N'Mercedes', 101000)
GO
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 4 chỗ (Hatchback)', N'Audi', 1111000)
GO
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 5 chỗ (CUV Gấm cao)', N'Porsche', 1111000)
GO
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 7 chỗ (SUV Gấm cao)', N'Honda', 190000)
go
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 7 chỗ (MPV Gấm thấp)', N'Honda', 190000)
go
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Bán tải', N'Honda', 200000)
go
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 19 chỗ', N'Honda', 100000)
go
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 23 chỗ (Sedan)', N'Vision', 30000)
go
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 25 chỗ', N'Yamaha', 300000)
go
INSERT INTO DBO.CATEGORY(name, brand, price)
VALUES (N'Xe 31 chỗ (CUV Gấm cao)', N'Honda', 500000)
go
---Customer
INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Nguyễn Văn A', N'Hà Nội', 178687678)
GO

INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Nguyễn Văn B', N'An Giang', 089838324)
GO

INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Nguyễn Văn C', N'TP Hồ Chí Minh', 0234938324)
GO

INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Nguyễn Văn D', N'TP Hồ Chí Minh', 034538324)
GO

INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Trần Tuấn E', N'Đà Nẵng', 089823424)
GO

INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Đặng Nguyễn Minh Thư', N'TP Hồ Chí Minh', 08778383)
GO

INSERT INTO DBO.CUSTOMER(fullname, address, mobile)
VALUES (N'Phan Văn Đạt', N'Bà Rịa - Vũng Tàu', 09220344)
GO

---Bill
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (1, 1, N'Xe 4 chỗ (Mini)', N'Bản đồ', N'Xăng', GETDATE(), Null, 0)
GO

INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (5, 1, N'Xe 7 chỗ (SUV Gấm cao)', N'Bản đồ', N'Xăng', GETDATE(), Null, 0)
GO


INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (6, 2, N'Xe 7 chỗ (MPV Gấm thấp)', N'Bản đồ', N'Xăng', GETDATE(), Null, 0)
GO

INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (7, 3, N'Bán tải', N'Cammera 360', N'Tất cả', GETDATE(), Null, 0)
GO

INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (2, 3, N'Xe 4 chỗ (Sedan)', N'Cammera 360', N'Tất cả', '2023-4-2' , '2023-5-2' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (2, 3, N'Xe 4 chỗ (Sedan)', N'Bluetooh', N'Xăng', '2023-5-2', '2023-5-3' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (1, 3, N'Xe 4 chỗ (Mini)', N'Bluetooh', N'Xăng', '2023-5-3' , '2023-5-3' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (4, 2, N'Xe 5 chỗ (CUV Gấm cao)', N'Bluetooh', N'Tất cả', '2023-4-3' , '2023-4-10' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (5, 4, N'Xe 7 chỗ (SUV Gấm cao)', N'Bluetooh, Camera 360', N'Điện', '2023-4-3' , '2023-4-10' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (5, 4, N'Xe 7 chỗ (SUV Gấm cao)', N'Bluetooh, Camera 360', N'Điện', '2023-11-3' , '2023-12-10' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (9, 5, N'Xe 23 chỗ', N'Camera 360', N'Điện', '2023-2-3' , '2023-3-11' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (11, 6, N'Xe 31 chỗ (CUV Gấm cao)', N'Camera 360', N'Điện', '2023-9-3' , '2023-9-22' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (10, 4, N'Xe 25 chỗ', N'Camera 360', N'Xăng', '2023-9-5' , '2023-9-29' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (7, 3, N'Bán tải', N'Camera 360', N'Xăng', '2023-12-17' , '2023-12-18' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (10, 6, N'Xe 25 chỗ', N'Camera 360', N'Tất cả', '2023-9-5' , '2023-9-12' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (10, 2, N'Xe 25 chỗ', N'Camera 360', N'Tất cả', '2023-12-5' , '2023-12-12' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (7, 1, N'Bán tải', N'Camera 360', N'Tất cả', '2023-12-17' , '2023-12-22' , 1)
GO
INSERT INTO DBO.bill(idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
VALUES (6, 7, N'Xe 7 chỗ (MPV Gấm thấp)', N'Camera 360', N'Điện', '2023-5-5' , '2023-5-20' , 1)
GO

--- PROC
CREATE PROC LOGIN
@username NVARCHAR(100), @password NVARCHAR(100)
AS
BEGIN
	SELECT * FROM DBO.ACCOUNT WHERE USERNAME = @username AND PASSWORD = @password
END
GO

CREATE PROC GetTableList
AS
BEGIN
	SELECT * FROM DBO.SCHEDULE
END
GO

CREATE TRIGGER NONEXISTBILL
ON dbo.Bill AFTER INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = id FROM Inserted
	
	DECLARE @idTable INT
	
	SELECT @idTable = idCategory FROM dbo.Bill WHERE id = @idBill

	DECLARE @count INT = 0 
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idCategory = @idTable AND status = 0
	IF (@count = 0)
	BEGIN
		UPDATE dbo.Schedule SET status = N'Trống' WHERE id = @idTable
	END
	ELSE
	BEGIN
		UPDATE dbo.Schedule SET status = N'Đang cho thuê' WHERE id = @idTable
	END
END
GO

CREATE PROC InsertBill
@idTable INT, @idCustomer INT,@type NVARCHAR(100), @feature NVARCHAR(100), @fuel NVARCHAR(100)
AS
BEGIN
	INSERT dbo.Bill 
	( idCategory, idCustomer, type, feature, fuel, DateCheckIn, DateCheckOut, status)
	VALUES  
	( @idTable, @idCustomer, @type, @feature, @fuel, GETDATE(), NULL, 0)
END
GO

CREATE PROC PROC_UpdateBill
@idBill INT, @idCustomer INT, @feature NVARCHAR(100), @fuel NVARCHAR(100)
AS
BEGIN
	UPDATE dbo.Bill	SET idCustomer = @idCustomer, fuel = @fuel, feature = @feature, status = 0 WHERE id = @idBill
END
GO

CREATE PROC PROC_GetBillByDate
@checkin date, @checkout date
AS
BEGIN
	SELECT b.type as [Type Car], c.brand as [Brand], c.price as [Price], b.DateCheckIn  as [Date check in], b.DateCheckOut  as [Date check out]  
	FROM dbo.Bill as b, dbo.Category as c
	WHERE b.DateCheckIn >= @checkin AND  b.DateCheckOut <= @checkout AND b.status = 1
	AND b.idCategory = c.id
END
GO

    

CREATE PROC PROC_UpdateAccount
@username NVARCHAR(100), @fullname NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE username = @username AND password = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET fullname = @fullname WHERE username = @username
		END		
		ELSE
		begin
			UPDATE dbo.Account SET fullname = @fullname, password = @newPassword WHERE username = @username
		end
	end
END
GO

CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END

CREATE TRIGGER ChangeStatusCar
ON dbo.Bill AFTER DELETE
AS
BEGIN
	DECLARE @idBill int
	SELECT @idBill = id FROM deleted
	declare @count int = 0
	declare @idCategory int = 0
	select @idCategory = idCategory from dbo.bill where id = @idBill
	--if (@idCategory != 0)
	begin
		update dbo.schedule set status = N'Trống' where id = @idCategory
	end
END
GO

CREATE PROC InsertCategoryByExcel
@name NVARCHAR(100), @brand NVARCHAR(100), @price float
AS
BEGIN
	declare @count int = 0
	select @count = count(*) from dbo.CATEGORY where name = @name

	if(@count = 0)
	begin
		INSERT dbo.category 
		( name, brand, price)
		VALUES  
		( @name, @brand, @price)
	end
	
END
GO

CREATE PROC InsertScheduleByExcel
@name NVARCHAR(100), @status NVARCHAR(100)
AS
BEGIN
	declare @count int = 0
	select @count = count(*) from dbo.SCHEDULE where name = @name

	if(@count = 0)
	begin
		INSERT dbo.schedule 
		( name, status)
		VALUES  
		( @name, @status)
	end
	
END
GO

drop table dbo.bill
drop table dbo.customer
drop table dbo.SCHEDULE
drop table dbo.CATEGORY
drop table dbo.account
select * from dbo.bill
select * from DBO.CATEGORY
select* from dbo.SCHEDULE
select * from dbo.CUSTOMER
select * from ACCOUNT

DROP TRIGGER NONEXISTBILL
DROP TRIGGER ChangeStatusCar
DROP PROC InsertBill
DROP PROC PROC_UpdateBill
DROP PROC PROC_UpdateAccount
DROP PROC PROC_GetBillByDate
DROP PROC LOGIN
DROP PROC GetTableList
DROP PROC InsertCategoryByExcel
DROP PROC InsertScheduleByExcel
