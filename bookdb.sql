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
