-- Flowly Task Management Database Schema (PostgreSQL) - SERIAL (Integer) ID Versiyası
-- Qeyd: Xarici açar (Foreign Key) asılılıqlarına görə cədvəllər doğru ardıcıllıqla yaradılır.

-- 1. Roles Table
CREATE TABLE Roles (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) UNIQUE NOT NULL,
    Description TEXT
);

-- Insert Default Roles
INSERT INTO Roles (Name, Description) VALUES 
('SuperAdmin', 'Sistemin tam idarəçisi'),
('Admin', 'Administrator'),
('Manager', 'Menencer'),
('Employee', 'İşçi');

-- 2. Departments Table
CREATE TABLE Departments (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) UNIQUE NOT NULL,
    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 3. Users Table
CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    UserName VARCHAR(100) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    AvatarUrl TEXT,
    RoleId INT NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    FailedLoginAttempts INT DEFAULT 0,
    DepartmentId INT,
    Position VARCHAR(100),
    LockoutEnd TIMESTAMP WITH TIME ZONE,
    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    CONSTRAINT FK_Users_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE SET NULL
);

    -- 20. PasswordResetTokens Table
    CREATE TABLE PasswordResetTokens (
        Id SERIAL PRIMARY KEY,
        UserId INT NOT NULL,
        Token VARCHAR(255) NOT NULL UNIQUE,
        ExpiresAt TIMESTAMP WITH TIME ZONE NOT NULL,
        IsUsed BOOLEAN DEFAULT FALSE,
        CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT FK_PasswordResetTokens_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
    );

    -- 21. RefreshTokens Tableewg
    CREATE TABLE RefreshTokens (
        Id SERIAL PRIMARY KEY,
        UserId INT NOT NULL,
        Token TEXT NOT NULL,
        Expires TIMESTAMP WITH TIME ZONE NOT NULL,
        CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
        IsRevoked BOOLEAN DEFAULT FALSE,
        CONSTRAINT FK_RefreshTokens_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
    );

    -- 22. Teams Table
    CREATE TABLE Teams (
        Id SERIAL PRIMARY KEY,
        Name VARCHAR(200) NOT NULL UNIQUE,
        CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
    );

    -- 23. TeamMembers Table
    CREATE TABLE TeamMembers (
        TeamId INT NOT NULL,
        UserId INT NOT NULL,
        JoinedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (TeamId, UserId),
        CONSTRAINT FK_TeamMembers_Teams FOREIGN KEY (TeamId) REFERENCES Teams(Id) ON DELETE CASCADE,
        CONSTRAINT FK_TeamMembers_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
    );


    --Task table 
    CREATE TABLE Tasks (
        Id SERIAL PRIMARY KEY,
        Title VARCHAR(255) NOT NULL,
        Description TEXT,
        Status VARCHAR(50) NOT NULL DEFAULT 'Pending', -- Pending, InProgress, Completed, OnHold, etc.
        Priority VARCHAR(50) NOT NULL DEFAULT 'Medium', -- Low, Medium, High, Urgent
        StartDate DATE,
        DueDate DATE,
        CompletedDate DATE,
        CreatedById INT NOT NULL,
        AssignedToId INT,
        DepartmentId INT,
        IsDeleted BOOLEAN DEFAULT FALSE,
        CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
        UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT FK_Tasks_Users_CreatedBy FOREIGN KEY (CreatedById) REFERENCES Users(Id),
        CONSTRAINT FK_Tasks_Users_AssignedTo FOREIGN KEY (AssignedToId) REFERENCES Users(Id),
        CONSTRAINT FK_Tasks_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
    );

-- -- TaskAttachments Table
-- CREATE TABLE TaskAttachments (
--     Id SERIAL PRIMARY KEY,
--     TaskId INT NOT NULL,
--     FileName VARCHAR(255) NOT NULL,
--     FilePath TEXT NOT NULL,
--     FileType VARCHAR(100),
--     FileSize BIGINT,
--     UploadedById INT NOT NULL,
--     UploadedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
--     CONSTRAINT FK_TaskAttachments_Tasks FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE,
--     CONSTRAINT FK_TaskAttachments_Users FOREIGN KEY (UploadedById) REFERENCES Users(Id)
-- );



