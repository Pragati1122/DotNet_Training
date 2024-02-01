# DotNet_Training
Intern training

Description of the projects:

1. bankproject: a menu driven console application for the manangement of a bank management system to manage the accounts of multiple users and the transactions in the bank.It provides functionalities like: Add Account, Deposit, Withdraw, Get Account details, Get All Accounts details and Get All transactions of user. Use of all CRUD operations in real time on Model's classes created in the app, implementation of LINQ and use of interfaces. Exception Handling is also added here.
   
2. bankproject_db: (Enhancement to the previous project) - a menu driven console application for the manangement of a bank management system to manage the accounts of multiple users and the transactions in the bank by storing the data in the database in real time using SSMS with the help of EFCore. CRUD operations performed in real time on the data stored in Sql Server Management System. Creating tables, relations among the tables and mapping of Database through EFCore are the major tasks performed here.
   
3. employeeproject: a console application for an employee management system. Features include to Accept and Display details for all Employees. GetDetails, Calculate Salary and Show details for Permanent Employees and Trainee Employees separately. Object Oriented concepts like Inheritance, overriding of function, dynamic polymorphism, iterfaces have been implemented here.

4. firstmvcproj: a basic mvc application to understand the use of EFCore in an mvc application.

5. flightproj: an MVC application for the management of a flight booking system. Use of EFCore to perform CRUD operations on the database in real time. Features include:
              - Login as a user or an admin
              - Functionalities for user : Register user, Login, Dynamic Logout, Search for flights (based on source, destination and date), Book A Flight, View Booking 
                                           Details, Cancel Booking
              - Functionalities for admin : View all flights of the portal, add a new flight, edit or delete a flight, view details of a flight
   Creating tables, relations, access of data from related tables and scaffolding of views are also performed.

6. flightthroughapi: (Enhanced version of previous project) Consits of two parts: a client side MVC application and a separate API to be consumed from the client side. No use of EFCore to perform CRUD operations i.e. there is no direct access of database from the client application instead a set of APIs are created which have a direct connection with the database and the client consumes these APIs for any operation on the database. Concept of Layering used in the API for enhanced security. Use of Repository and Services layer for the transfer of data in the API controller methods.
                                           
