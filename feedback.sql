use BookStoreDB

--- Table For Feedback----
create Table Feedback
(
	FeedbackId int identity(1,1) not null primary key,
	Comment varchar(max) not null,
	Rating int not null,
	BookId int not null 
	foreign key (BookId) references BookTable(BookId),
	UserId INT not null
	foreign key (UserId) references Users(UserId),
);

select *from Feedback 

---Add Feedback Store Procedure-----
create or alter Proc spAddFeedback
(
	@Comment varchar(max),
	@Rating int,
	@BookId int,
	@UserId int
)
as
Declare @AverageRating int;
begin
	if (exists(SELECT * FROM Feedback where BookId = @BookId and UserId=@UserId))
		select 1;
	Else
	Begin
		if (exists(SELECT * FROM BookTable WHERE BookId = @BookId))
		Begin  select * from Feedback
			Begin try
				Begin transaction
					Insert into Feedback(Comment, Rating, BookId, UserId) values(@Comment, @Rating, @BookId, @UserId);		
					set @AverageRating = (Select AVG(Rating) from Feedback where BookId = @BookId);
					Update BookTable set TotalRating = @AverageRating,  RatingCount = RatingCount + 1 
								 where  BookId = @BookId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
end;


-----Get All Feedback By Book ID Refereing Userid store procedure-----
create or alter Proc spGetFeedback
(
	@BookId int
)
as
begin
	Select FeedbackId, Comment, Rating, BookId, u.FullName
	From Users u
	Inner Join Feedback f
	on f.UserId = u.UserId
	where
	 BookId = @BookId;
end;