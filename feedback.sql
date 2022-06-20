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
