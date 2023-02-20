use Banking_System;
begin transaction;

insert into Employee (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Bram', 'Bloxham', '631792321', 'bbloxham0@rediff.com','Macpherson Pass', 2, 'Hengpi', 15500);
insert into Employee (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Vernor', 'Dekeyser', '212316423', 'vdekeyser1@oakley.com', 'Morrow Center', 123, 'London', 25252);
insert into Employee (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Jourdan', 'Lujan', '414655176', 'jlujan2@barnesandnoble.com', 'Daystar Circle', 2008, 'Hronov', 12389);
insert into Employee (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Justine', 'Petrie', '272634004', 'jpetrie3@deliciousdays.com', 'Katie Point', 95, 'New York', 98902);
insert into Employee (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Frasier', 'Royson', '155565619', 'froyson4@whitehouse.gov', 'Saint Paul Hill', 213, 'Grabovci', 21332);


insert into Customer (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Jorie', 'Mordanti', '246106058', 'jmordanti0@accuweather.com', 'Hovde Street',5764,'Sadar',23152);
insert into Customer (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Kirbee', 'Ledwidge', '143251977', 'kledwidge1@tinypic.com', 'Meadow Ridge Junction',58278, 'Bantog', 32121);
insert into Customer (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Delano', 'Weavers', '587870578', 'dweavers2@cdc.gov', 'Talmadge Way',78, 'Sumberpandan', 34215);
insert into Customer (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Sherrie', 'Heam', '847298112', 'sheam3@oakley.com', 'Graceland Way', 243,'Rahayu', 26932);
insert into Customer (first_name, last_name, phone_number, email_address, address_street,address_street_number,address_city,address_zip) values ('Katrinka', 'Kenewel', '391743251', 'kkenewel4@ehow.com', 'Comanche Road',8, 'Nova Kakhovka', 63636);

insert into Meeting (Employee_id, Customer_id,short_description,text,request_created_date,meeting_date) values (1,1,'Bank account creation', 'I want to create a new bank account.', '2022-01-03', '2022-01-04');
insert into Meeting (Employee_id, Customer_id,short_description,text,request_created_date,meeting_date) values (2,1,'Bank account closure', 'I want to close my bank account.', '2022-01-07', '2022-01-07');
insert into Meeting (Employee_id, Customer_id,short_description,text,request_created_date,meeting_date) values (1,2,'Opening a savings account', 'I want to setup a new savings account.', '2022-01-06', '2022-01-10');


insert into Personal_Account_Type (type_name,maintenance_fee) values ('Personal Normal', 3);
insert into Personal_Account_Type (type_name,maintenance_fee) values ('Personal Plus', 1.5);

insert into Savings_Account_Type (type_name,interest_rate) values ('Savings Normal', 3);
insert into Savings_Account_Type (type_name,interest_rate) values ('Savings Plus', 5);

insert into Card_Brand (name) values ('Master Card');
insert into Card_Brand (name) values ('Visa');
insert into Card_Brand (name) values ('American Express');

insert into Card (Account_id, Card_Brand_id, card_number,issue_date,expire_date, isDebit) values (1,1, 1234567891011121, '2022-01-01', '2023-01-01',1);
insert into Card (Account_id, Card_Brand_id, card_number,issue_date,expire_date, isDebit) values (2,2, 2252267891468122, '2022-02-02', '2023-02-02',1);
insert into Card (Account_id, Card_Brand_id, card_number,issue_date,expire_date, isDebit) values (3,3, 5234567891321126, '2022-03-04', '2023-03-03',1);
insert into Card (Account_id, Card_Brand_id, card_number,issue_date,expire_date, isDebit) values (1,3, 3234567895231123, '2022-04-04', '2023-04-04',0);



insert into Currency (name, sign, usd_conversion_rate) values ('United States Dollar', '$', 1);
insert into Currency (name, sign, usd_conversion_rate) values ('Euro', '€', 0.92);
insert into Currency (name, sign, usd_conversion_rate) values ('Canadian Dollar','$',1.332069);

insert into Account(account_number,Savings_Account_Type_id, Personal_Account_Type_id, Customer_id, Currency_id, balance) values (55233,1, null, 1,1, 500);
insert into Account(account_number,Savings_Account_Type_id, Personal_Account_Type_id, Customer_id, Currency_id, balance) values (33223,2, null, 1,2, 2500);
insert into Account(account_number,Savings_Account_Type_id, Personal_Account_Type_id, Customer_id, Currency_id, balance) values (21331,null, 1, 2,1, 1500);
insert into Account(account_number,Savings_Account_Type_id, Personal_Account_Type_id, Customer_id, Currency_id, balance) values (92123,1, null, 3,3, 6500);

insert into Bank_Transaction(from_Account_id, to_Account_id, amount, transaction_date, note) values (1,2,500,'2022-01-01',null);
insert into Bank_Transaction(from_Account_id, to_Account_id, amount, transaction_date, note) values (1,3,300,'2022-02-02','Rent');
insert into Bank_Transaction(from_Account_id, to_Account_id, amount, transaction_date, note) values (2,3,150,'2022-03-03',null);
insert into Bank_Transaction(from_Account_id, to_Account_id, amount, transaction_date, note) values (3,4,200,'2022-04-04',null);
insert into Bank_Transaction(from_Account_id, to_Account_id, amount, transaction_date, note) values (3,1,500,'2022-05-05','Happy birthday');

commit;