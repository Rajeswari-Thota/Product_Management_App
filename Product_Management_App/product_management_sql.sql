use mydb
create table Product_Management
(
Product_Id int identity(1,1) primary key,
Product_Name varchar(50),
Product_Description varchar(100),
Quantity int,
Price decimal
)
insert into Product_Management ("Product_Name","Product_Description","Quantity","Price") values('laptop','laptop of brand hp',3,35000)
select*from Product_Management