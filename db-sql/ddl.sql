Create database Banking_System;

Use Banking_System;

SET IMPLICIT_TRANSACTIONS ON;

BEGIN TRANSACTION;



CREATE TABLE Savings_Account_Type 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     type_name VARCHAR (60) NOT NULL unique, 
     interest_rate NUMERIC (28,2) NOT NULL 
    )

CREATE TABLE Personal_Account_Type 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     type_name VARCHAR (60) NOT NULL unique, 
     maintenance_fee NUMERIC (28,2) NOT NULL 
    )

CREATE TABLE Card_Brand 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     name VARCHAR (40) NOT NULL unique
    )

CREATE TABLE Currency 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     name VARCHAR (40) NOT NULL unique, 
     sign CHAR (1) NOT NULL , 
     usd_conversion_rate DECIMAL (28,3) NOT NULL 
    )

CREATE TABLE Customer 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     first_name VARCHAR (50) NOT NULL , 
     last_name VARCHAR (50) NOT NULL , 
     phone_number NUMERIC (9) NOT NULL unique, 
     email_address VARCHAR (70) NOT NULL unique, 
     address_street VARCHAR (60) NOT NULL , 
     address_street_number int NOT NULL , 
     address_city VARCHAR (60) NOT NULL , 
     address_zip int NOT NULL 
    )



CREATE TABLE Account 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     Savings_Account_Type_id int FOREIGN KEY references Savings_Account_Type(id) , 
     Personal_Account_Type_id int FOREIGN KEY references Personal_Account_Type(id) , 
     Customer_id int NOT NULL FOREIGN KEY references Customer(id), 
     Currency_id int NOT NULL FOREIGN KEY references Currency(id) , 
     account_number int not null unique,
	 balance NUMERIC (28,2) NOT NULL 
	)


ALTER TABLE Account 
    ADD CONSTRAINT Arc_1 CHECK ( 
        (  (Savings_Account_Type_id IS NOT NULL) AND 
         (Personal_Account_Type_id IS NULL) ) OR 
        (  (Personal_Account_Type_id IS NOT NULL) AND 
         (Savings_Account_Type_id IS NULL) )  ) ;


CREATE TABLE Card 
    (
     id int NOT NULL IDENTITY PRIMARY KEY, 
     Account_id int NOT NULL , 
     Card_Brand_id int NOT NULL FOREIGN KEY REFERENCES Card_Brand(id), 
     card_number NUMERIC (16) NOT NULL unique, 
     issue_date DATE NOT NULL , 
     expire_date DATE NOT NULL , 
     isDebit BIT NOT NULL,
	
	constraint Valid_Expire_Date check(expire_date > issue_date)
    )


CREATE TABLE Employee 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     first_name VARCHAR (50) NOT NULL , 
     last_name VARCHAR (50) NOT NULL , 
     phone_number NUMERIC (9) NOT NULL unique, 
     email_address VARCHAR (70) NOT NULL unique, 
     address_street VARCHAR (60) NOT NULL , 
     address_street_number int NOT NULL , 
     address_city VARCHAR (60) NOT NULL , 
     address_zip int NOT NULL 
    )

CREATE TABLE Meeting 
    (
     id int NOT NULL IDENTITY PRIMARY KEY, 
     Employee_id int NOT NULL FOREIGN KEY REFERENCES Employee(id), 
     Customer_id int NOT NULL FOREIGN KEY REFERENCES Customer(id), 
     short_description VARCHAR (100) NOT NULL , 
     text VARCHAR (500) NOT NULL , 
     request_created_date DATE NOT NULL , 
     meeting_date DATE,
	 constraint Valid_Meeting_Date check (request_created_date <= meeting_date)
    )



CREATE TABLE Bank_Transaction 
    (
     id int NOT NULL IDENTITY PRIMARY KEY , 
     from_Account_id int NOT NULL FOREIGN KEY REFERENCES Account(id), 
     to_Account_id int NOT NULL FOREIGN KEY REFERENCES Account(id), 
     amount NUMERIC (28,2) NOT NULL , 
     transaction_date DATE NOT NULL , 
     note VARCHAR (100)
    )


	
CREATE TABLE Role 
    (
     id int NOT NULL PRIMARY KEY IDENTITY, 
     name VARCHAR (60) NOT NULL 
    )


CREATE TABLE User_Account 
    (
     id int NOT NULL PRIMARY KEY IDENTITY, 
     Employee_id int foreign key references Employee(id), 
     Customer_id int foreign key references Customer(id) , 
     username VARCHAR (60) NOT NULL unique, 
     password VARCHAR (MAX) NOT NULL 
    )

	
CREATE TABLE Account_Role 
    (
     id int NOT NULL IDENTITY PRIMARY KEY, 
     Role_id int NOT NULL FOREIGN KEY references Role(id), 
     User_Account_id int NOT NULL FOREIGN KEY references User_Account(id) 
    )

ALTER TABLE User_Account 
    ADD CONSTRAINT FKArc_4 CHECK ( 
        (  (Employee_id IS NOT NULL) AND 
         (Customer_id IS NULL) ) OR 
        (  (Customer_id IS NOT NULL) AND 
         (Employee_id IS NULL) ));

COMMIT;