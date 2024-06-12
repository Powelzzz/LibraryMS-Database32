CREATE DATABASE Librar

USE Librar;

CREATE TABLE NewBook (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    bName NVARCHAR(100),
    bAuthor NVARCHAR(100),
    bPubl NVARCHAR(100),
    bPdate DATE,
    bPrice DECIMAL(10, 2),
    bQuan INT
);

CREATE TABLE NewStudent (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    sname NVARCHAR(100),
    enroll NVARCHAR(10),
    dep NVARCHAR(100),
    sem NVARCHAR(50),
    contact BIGINT,
    email NVARCHAR(100)
);

CREATE TABLE IRBook (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    book_title NVARCHAR(100),
    author NVARCHAR(100),
    publication NVARCHAR(100),
    publish_date DATE,
    book_price DECIMAL(10, 2),
    book_quantity INT,
    book_issue_date DATE,
    book_return_date DATE
);

CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    StudentName NVARCHAR(100),
    Department NVARCHAR(100),
    Semester NVARCHAR(50),
    Contact BIGINT,
    Email NVARCHAR(100),
    CONSTRAINT UC_StudentEmail UNIQUE (Email)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(100) 
);




