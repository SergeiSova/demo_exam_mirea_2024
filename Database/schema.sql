IF DB_ID(N'Sova') IS NULL
    CREATE DATABASE Sova;
GO
USE Sova;
GO

CREATE TABLE Users (
    id INT IDENTITY PRIMARY KEY,
    Login NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    Post NVARCHAR(20) NOT NULL CHECK (Post IN (N'Users', N'Executor', N'Manager'))
);

CREATE TABLE Executor (
    id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE Entity (
    id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Phone NVARCHAR(30) NOT NULL,
    Email NVARCHAR(150) NULL,
    Device NVARCHAR(150) NOT NULL,
    SN NVARCHAR(100) NULL,
    CommentsUser NVARCHAR(1000) NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT N'В ожидании',
    Executor NVARCHAR(150) NULL REFERENCES Executor(Name),
    RepairType NVARCHAR(200) NULL,
    FaultDescription NVARCHAR(1000) NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    Comment NVARCHAR(1000) NULL,
    CONSTRAINT CK_Entity_Dates CHECK (EndDate IS NULL OR StartDate IS NULL OR EndDate >= StartDate)
);
GO

-- Учебные учётные записи для локальной проверки. Пароль у всех: 111
INSERT INTO Users (Login, PasswordHash, Post) VALUES
    (N'user', N'pbkdf2$100000$sCioqlRxUKLDCJhR3Cy7Vw==$kDZS+zHhO+nBKESVLKrB1LMqzpYg6ke+qpW5f0Kx7Yg=', N'Users'),
    (N'executor', N'pbkdf2$100000$+Li6z0iUABta9pGuhvymSw==$s+I38+Dlvlq6nJNiTMOyBmuHEc/QmdJxbI+2aez78XY=', N'Executor'),
    (N'manager', N'pbkdf2$100000$ScDkZU8QIl1u8SBIUBWzNg==$NmyxROgz59cvT5VNEi/f1O9HeCrWeTysJV9KuIr5fqQ=', N'Manager');

INSERT INTO Executor (Name) VALUES (N'Иванов И.И.'), (N'Петров П.П.');
GO
