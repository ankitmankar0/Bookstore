----Creating Order Table------
use BookStore;

create table Orders(
	OrdersId int identity(1,1) not null primary key,
	TotalPrice int not null,
	OrderBookQuantity int not null,
	OrderDate Date not null,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references BookTable(BookId),
	AddressId int not null foreign key (AddressId) references AddressTable(AddressId)
);
select * from Orders;

drop table Orders


---Adding Orders Store Procedure----
create or alter Proc spAddOrder
(
	@OrderBookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
begin
	set @TotalPrice = (select DiscountPrice from BookTable where BookId = @BookId);
	If(Exists(Select * from BookTable where BookId = @BookId))
		begin
			If(Exists (Select * from Users where UserId = @UserId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orders(TotalPrice, OrderBookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@OrderBookQuantity, @OrderBookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update BookTable set BookQuantity=BookQuantity-@OrderBookQuantity where BookId = @BookId;
						Delete from CartTable where BookId = @BookId and UserId = @UserId;
						select * from Orders;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				end
			Else
				Begin
					Select 3;
				End
		End
	Else
		Begin
			Select 2;
		End
end;




---Get ALl Orders By UserId=----
Create or Alter Proc spGetOrders
(
	@UserId int
)
as
begin
		Select 
		O.OrdersId, O.UserId, O.AddressId, b.bookId,
		O.TotalPrice, O.OrderBookQuantity, O.OrderDate,
		b.BookName, b.AuthorName, b.BookImage
		FROM BookTable b
		inner join Orders O on O.BookId = b.BookId 
		where 
			O.UserId = @UserId;
end;