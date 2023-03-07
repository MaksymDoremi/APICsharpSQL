create table Customer
(
	[ID] int primary key identity(1,1),
	[Name] varchar(40) not null,
	[Surname] varchar(40) not null,
	[Email] varchar(40) unique not null
);



go

create table Manufacturer
(
	[ID] int primary key identity(1,1),
	[Name] varchar(40) not null,
	[Email] varchar(40) unique not null
);

go

create table [Type]
(
	[ID] int primary key identity(1,1),
	[Name] varchar(40) unique not null
);

go

create table Product
(
	[ID] int primary key identity(1,1),
	[Manufacturer_ID] int foreign key references [Manufacturer](ID) not null,
	[Type_ID] int foreign key references [Type](ID) not null,
	[Name] varchar(40) unique not null,
	[Price] float not null check(Price > 0)
	
);

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Manufacturer] FOREIGN KEY([Manufacturer_ID])
REFERENCES [dbo].[Manufacturer] ([ID]) 
ON DELETE CASCADE

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Type] FOREIGN KEY([Type_ID])
REFERENCES [dbo].[Type] ([ID]) 
ON DELETE CASCADE


go

create table [Order]
(
	[ID] int primary key identity(1,1),
	[Customer_ID] int foreign key references [Customer](ID) not null,
	[Datetime] datetime not null,
	[Price] float not null check(Price > 0),
	[Delivered] bit not null
);

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([Customer_ID])
REFERENCES [dbo].[Customer] ([ID]) 
ON DELETE CASCADE

go

create table Item
(
	[ID] int primary key identity(1,1),
	[Order_ID] int foreign key references [Order](ID) not null,
	[Product_ID] int foreign key references [Product](ID) not null,
	[Count] int not null check([Count] > 0),
	[Price] float not null check(Price > 0)
);

ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Order] FOREIGN KEY([Order_ID])
REFERENCES [dbo].[Order] ([ID]) 
ON DELETE CASCADE

ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([ID]) 
ON DELETE CASCADE
