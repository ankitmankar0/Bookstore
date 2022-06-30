
---Create book table
create table BookTable(
BookId int identity (1,1)primary key,
BookName varchar(255),
AuthorName varchar(255),
TotalRating int,
RatingCount int,
OriginalPrice decimal,
DiscountPrice decimal,
BookDetails varchar(255),
BookImage varchar(255),
BookQuantity int
);

select *from BookTable;

create procedure SPAddBook
(
@BookName varchar(255),
@AuthorName varchar(255),
@TotalRating int,
@RatingCount int,
@OriginalPrice decimal,
@DiscountPrice decimal,
@BookDetails varchar(255),
@BookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Insert into BookTable(BookName, AuthorName, TotalRating, RatingCount, OriginalPrice, 
DiscountPrice, BookDetails, BookImage, BookQuantity)
values (@BookName, @AuthorName, @TotalRating, @RatingCount ,@OriginalPrice, @DiscountPrice,
@BookDetails, @BookImage, @BookQuantity
);
End;

---create procedure to getbookbybookid
create procedure spGetBookByBookId
(
@BookId int
)
as
BEGIN
select * from BookTable
where BookId = @BookId;
End;

---Procedure to deletebook
create procedure spDeleteBook
(
@BookId int
)
as
BEGIN
Delete BookTable 
where BookId = @BookId;
End;

--procedure to updatebook
create procedure spUpdateBook
(
@BookId int,
@BookName varchar(255),
@AuthorName varchar(255),
@TotalRating int,
@RatingCount int,
@OriginalPrice Decimal,
@DiscountPrice Decimal,
@BookDetails varchar(255),
@BookImage varchar(255),
@BookQuantity int
)
as
BEGIN
Update BookTable set BookName = @BookName, 
AuthorName = @AuthorName,
TotalRating = @TotalRating,
RatingCount = @RatingCount,
OriginalPrice= @OriginalPrice,
DiscountPrice = @DiscountPrice,
BookDetails = @BookDetails,
BookImage =@BookImage,
BookQuantity = @BookQuantity
where BookId = @BookId;
End;


-- create procedure to get all book 
create procedure spGetAllBook
as
BEGIN
	select * from BookTable;
End;

---------------------------------------------------------------------------------End Of Book -----------------------------------------------------------------------

create Table CartTable
(
CartId int primary key identity(1,1),
BooksQty int,
UserId int Foreign Key References Users(UserId),
BookId int Foreign Key References BookTable(BookId)
);

select * from CartTable;

DROP TABLE CartTable;

---create procedure to addcart
create Procedure spAddCart
(
@BooksQty int,
@UserId int,
@BookId int
)
As
Begin
Insert into CartTable (BooksQty,UserId,BookId) 
values (@BooksQty,@UserId,@BookId);
End;


DROP PROCEDURE spAddCart;
GO

---Create procedure to deletecart
create procedure spDeleteCart
@CartId int
As
Begin 
Delete CartTable where CartId = @CartId
End

---create procedure to UpdateCart
create procedure spUpdateCart
@BooksQty int,
@CartId int
As
Begin
update CartTable set BooksQty = @BooksQty
where CartId = @CartId
End

--create procedure to GetAllBookinCart by UserId
create procedure spGetAllBookinCart
@UserId int
As
Begin
select CartTable.CartId,CartTable.UserId,CartTable.BookId,CartTable.BooksQty,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from CartTable inner join BookTable on CartTable.BookId = BookTable.BookId
where CartTable.UserId = @UserId
End


---------------------------------------------------------------------------------End Book Cart --------------------------------------------------------------------------

---create wishlist table
create Table WishlistTable
(
WishlistId int identity(1,1) primary key,
UserId INT FOREIGN KEY REFERENCES Users(UserId),
BookId INT FOREIGN KEY REFERENCES BookTable(BookId)
);

select * from WishlistTable


---create procedure to Add in Wishlist
create procedure spAddInWishlist
@UserId int,
@BookId int
As
Begin
Insert Into WishlistTable (UserId,BookId) values (@UserId,@BookId)
End
DROP PROCEDURE spAddInWishlist;
GO

--create procedure to delete from wishlist
create procedure spDeleteFromWishlist
@WishListId int
As
Begin
Delete WishlistTable where WishListId=@WishListId
End

---create procedure to get all Books from wishlist
create procedure spGetAllBooksinWishList
@UserId int
As
Begin
select WishlistTable.WishListId,WishlistTable.UserId,WishlistTable.BookId,
BookTable.BookName,BookTable.AuthorName,BookTable.TotalRating,BookTable.RatingCount,BookTable.OriginalPrice,BookTable.DiscountPrice,BookTable.BookDetails,BookTable.BookImage,BookTable.BookQuantity 
from WishlistTable inner join BookTable on WishlistTable.BookId=BookTable.BookId
where WishlistTable.userId=@UserId
End