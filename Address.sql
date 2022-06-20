---create address type table
create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(255)
);
select * from AddressTypeTable


---insert record for addresstype table
insert into AddressTypeTable values('Home'),('Office'),('Other');


---create address table
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address varchar(255),
City varchar(100),
State varchar(100),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
select * from AddressTable


DROP TABLE AddressTable;


---create procedure to AddAddress
--- Procedure To Add Address
create procedure spAddAddress
(
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Insert into AddressTable 
values(@Address, @City, @State, @TypeId, @UserId);
end
Else
begin
select 2
end
End;

--create procedure for updateAddress
create procedure spUpdateAddress
(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
Else
begin
select 2
end
End;


--create procedure to delete address
create Procedure spDeleteAddress
(
@AddressId int
)
as
BEGIN
Delete AddressTable where AddressId = @AddressId 
End;

-- Procedure To Get All Address By UserId
create Procedure spGetAddressByUserId
(
@UserId int
)
as
BEGIN
Select Address, City, State,a1.UserId, a2.TypeId
from AddressTable a1
Inner join AddressTypeTable a2 on a2.TypeId = a1.TypeId 
where UserId = @UserId;
END;

-- Procedure To Get All Address
create Procedure spGetAllAddress
As
Begin
select * from AddressTable
End