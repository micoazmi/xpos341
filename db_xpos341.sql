
create table TblCategory (
Id int primary key not null identity(1,1),
NameCategory varchar(50) null,
Description varchar(max) null,
IsDelete bit null,
CreateBy int not null,
CreateDate datetime not null,
UpdateBy int null,
UpdateDate datetime null
)

select * from tblcategory

select * from TblVariant

insert into TblCategory
values
('Drink', 'Desc of Drink', 0, 1, GETDATE(), null, null),
('Desert', 'Desc of Desert', 0, 1, GETDATE(), null, null)

create table TblProduct (
Id int primary key not null identity(1,1),
IdVariant int not null,
NameProduct varchar(100) not null,
Price decimal(18,0) not null,
Stock int not null,
Image varchar(max) null,
IsDelete bit null,
CreateBy int not null,
CreateDate datetime not null,
UpdateBy int null,
UpdateDate datetime null
)

insert into TblProduct
values
(2, 'Jus Melon Ceria', 10000, 10, null, 0, 1, GETDATE(), null, null)

select * from TblProduct

update TblProduct set Image='asus_tuf.jpg' where id=7

delete TblProduct where id > 1

create table TblOrderHeader (
Id int primary key not null identity(1,1),
CodeTransaction nvarchar(20) not null,
IdCustomer int not null,
Amount decimal(18,2) not null,
TotalQty int not null,
IsCheckout bit not null,
IsDelete bit null,
CreateBy int not null,
CreateDate datetime not null,
UpdateBy int null,
UpdateDate datetime null
)

create table TblOrderDetail (
Id int primary key not null identity(1,1),
IdHeader int not null,
IdProduct int not null,
Qty int not null,
SumPrice decimal(18,2) not null,
IsDelete bit null,
CreateBy int not null,
CreateDate datetime not null,
UpdateBy int null,
UpdateDate datetime null
)

create table TblCustomer(
Id int primary key not null identity(1,1),
NameCustomer nvarchar(50) not null,
Email nvarchar(50) not null,
Password nvarchar(100) not null,
Address nvarchar(max) not null,
Phone nvarchar(15) not null,
IdRole int null,
IsDelete bit not null,
CreateBy int not null,
CreateDate datetime not null,
UpdateBy int null,
UpdateDate datetime null
)

insert into TblCustomer
values
('admin', 'admin@example.com', 'admin', 'Jakarta', '089912345678', 1, 0, 1, GETDATE(), null, null)

create table TblRole(
	Id int primary key identity(1,1) not null,
	RoleName varchar(80),
	IsDelete bit,
	CreatedBy int not null,
	CreatedDate datetime not null,
	UpdatedBy int,
	UpdatedDate datetime
)

insert into TblRole
values
('Administrator', null, 1, GETDATE(), null, null)

update TblRole set IsDelete=0

select * from TblCustomer
select * from TblRole

create table TblMenu(
	Id int primary key identity(1,1) not null,
	MenuName varchar(80) not null,
	MenuAction varchar(80) not null,
	MenuController varchar(80) not null,
	MenuIcon varchar(80) null,
	MenuSorting int null,
	IsParent bit null,
	MenuParent int null,
	IsDelete bit null,
	CreatedBy int not null,
	CreatedDate datetime not null,
	UpdatedBy int null,
	UpdatedDate datetime null
)

create table TblMenuAccess(
	Id int primary key identity(1,1) not null,
	RoleId int null,
	MenuId int null,
	IsDelete bit null,
	CreatedBy int not null,
	CreatedDate datetime not null,
	UpdatedBy int null,
	UpdatedDate datetime null
)

select * from TblMenu
select * from TblMenuAccess

SET IDENTITY_INSERT [dbo].[TblMenu] ON 

INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Home', N'Index', N'Home', N'home', 1, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Friend', N'Index', N'Friend', N'tag', 2, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'CategoryTry', N'Index', N'CategoryTry', N'calendar', 3, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'VariantTry', N'Index', N'VariantTry', N'message-square', 4, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Category', N'Index', N'Category', N'tag', 5, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Variant', N'Index', N'Variant', N'calendar', 6, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Product', N'Index', N'Product', N'message-square', 7, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Menu Catalog', N'Catalog', N'Order', N'tag', 8, 0, 16, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'History Order', N'HistoryOrder', N'Order', N'message-square', 9, 0, 16, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Product Kalbe', N'Index', N'ProductKalbe', N'tag', 13, 0, 16, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Customer Kalbe', N'Index', N'CustomerKalbe', N'calendar', 14, 0, 16, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Penjualan Kalbe', N'Index', N'PenjualanKalbe', N'message-square', 15, 0, 16, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'Role', N'Index', N'Role', N'tag', 10, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N'Customer', N'Index', N'Customer', N'calendar', 12, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, N'Master', N'', N'', N'tag', 1, 1, 0, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, N'Transaction', N'', N'', N'calendar', 2, 1, 0, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, N'Atur Menu Access', N'Index_MenuAccess', N'Role', N'tag', 11, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenu] ([Id], [MenuName], [MenuAction], [MenuController], [MenuIcon], [MenuSorting], [IsParent], [MenuParent], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (18, N'Spesialisasi Dokter', N'Index', N'MSpecialization', N'calendar', 16, 0, 15, 0, 1, CAST(N'2022-10-30T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TblMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[TblMenuAccess] ON 

INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, 1, 1, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, 1, 2, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, 1, 3, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, 1, 4, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, 1, 5, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, 1, 6, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, 1, 7, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, 1, 8, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, 1, 9, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, 1, 10, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, 1, 11, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, 1, 12, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, 1, 13, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, 1, 14, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, 1, 15, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, 1, 16, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
INSERT [dbo].[TblMenuAccess] ([Id], [RoleId], [MenuId], [IsDelete], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, 1, 17, 0, 1, CAST(N'2023-04-25T01:41:57.863' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TblMenuAccess] OFF
GO
