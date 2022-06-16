create database BookStore;

use BookStore;


create table Users
(
UserId int IDENTITY(1,1) PRIMARY KEY,
FullName varchar(255),
Email varchar(255),
Password varchar(255),
MobileNumber bigint
);


select * from Users;

---stored procedures for User Api
---Create procedured for User Registration

Create procedure spUserRegister       
(        
    @FullName varchar(255),
    @Email varchar(255),
    @Password varchar(255),
    @MobileNumber bigint       
)
as         
Begin         
    Insert into Users (FullName,Email,Password,MobileNumber)         
    Values (@FullName,@Email,@Password,@MobileNumber);        
End

---Create procedured for User Login
create procedure spUserLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Users
where Email = @Email and Password = @Password
End;

create procedure spUserForgotPassword
(
@Email varchar(Max)
)
as
begin
Update Users
set Password = 'Null'
where Email = @Email;
select * from Users where Email = @Email;
End;


---create procedure for user reset password 
create procedure spUserResetPassword
(
@Email varchar(Max),
@Password varchar(Max)
)
AS
BEGIN
UPDATE Users 
SET 
Password = @Password 
WHERE Email = @Email;
End;

--------------------------------------------------------------------------------------------Admin Table----------------------------------------------------------------------------------
create Table Admins
(
	AdminId int Identity(1,1) primary key not null,
	FullName varchar(255) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	MobileNumber varchar(50) not null,
);

select * from Admins


INSERT INTO Admins VALUES ('Admin Ankit','admin@bookstore.com', 'Admin@23', '+91 9175739197');


Create Proc LoginAdmin
(
	@Email varchar(max),
	@Password varchar(max)
)
as
BEGIN
	If(Exists(select * from Admins where Email= @Email and Password = @Password))
		Begin
			select * from Admins where Email= @Email and Password = @Password;
		end
	Else
		Begin
			select 2;
		End
END;


------------------------------------------------------------------------------------------------------------------------------------------------------------------


