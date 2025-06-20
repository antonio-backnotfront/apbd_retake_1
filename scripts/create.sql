-- Create tables in the correct order
CREATE TABLE AcademicTitle (
    Id INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(100) NOT NULL UNIQUE,
    RequiredLevel INT NOT NULL,
    PRIMARY KEY (Id)
);

CREATE TABLE Teacher (
    Id INT IDENTITY(1,1) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    MiddleName VARCHAR(100),
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(200) NOT NULL UNIQUE,
    AcademicTitleId INT NOT NULL, 
    Level INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (AcademicTitleId) REFERENCES AcademicTitle(Id)
);

CREATE TABLE RecordType (
    Id INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(100) NOT NULL UNIQUE,
    PRIMARY KEY (Id)
);

CREATE TABLE Task (
    Id INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(2000) NOT NULL,
    RequiredLevel INT,
    MinRequiredTime DECIMAL(6),
    PRIMARY KEY (Id)
);

CREATE TABLE Records (
    Id INT IDENTITY(1,1) NOT NULL,
    TeacherId INT NOT NULL,
    RecordTypeId INT NOT NULL,
    TaskId INT NOT NULL,
    ExecutionTime DECIMAL(6) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (TeacherId) REFERENCES Teacher(Id),
    FOREIGN KEY (RecordTypeId) REFERENCES RecordType(Id),
    FOREIGN KEY (TaskId) REFERENCES Task(Id)
);
