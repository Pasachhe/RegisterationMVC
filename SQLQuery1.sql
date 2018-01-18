CREATE TABLE [dbo].[UserRole] (
    [RoleId]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);




CREATE TABLE [dbo].[SiteUser] (
    [UserId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (50)  NULL,
    [EmailId]  NVARCHAR (50)  NULL,
    [Password] NVARCHAR (50)  NULL,
    [Address]  NVARCHAR (150) NULL,
    [RoleId]   INT            NULL,
    CONSTRAINT [PK_SiteUser] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_SiteUser_UserRole] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRole] ([RoleId])
);


CREATE TABLE [dbo].[Department] (
    [DepartmentId]   INT            IDENTITY (1, 1) NOT NULL,
    [DepartmentName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([DepartmentId] ASC)
);


--Drop table Attendance

--Drop table Employee

CREATE TABLE [dbo].[Employee] (
    [EmployeeId]   INT IDENTITY (1, 1) NOT NULL,
    [UserId]       INT NULL,
    [DepartmentId] INT NULL,
	[DesId] INT NULL,
    [Salary]       INT NULL,
    [IsDeleted]    BIT NULL,
    [MinWorkHr]    INT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]),
    CONSTRAINT [FK_Employee_Designition] FOREIGN KEY ([DesId]) REFERENCES [dbo].[Designition] ([DesId]),
	CONSTRAINT [FK_Employee_SiteUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SiteUser] ([UserId])
);



CREATE TABLE [dbo].[Attendance] (
    [AttendanceId] NVARCHAR (50) NOT NULL,
    [EmployeeId]   INT           NULL,
    [Date]         DATE          NULL,
    [InTime]       DATETIME      NULL,
    [OutTime]      DATETIME      NULL,
    [WorkHours]    TIME (7)      NULL,
    PRIMARY KEY CLUSTERED ([AttendanceId] ASC),
    CONSTRAINT [FK_Attendance_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId])
);




CREATE TABLE [dbo].[Project] (
    [ProjectId]   INT            IDENTITY (1, 1) NOT NULL,
    [ProjectName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);


CREATE TABLE [dbo].[Designition] (
    [DesId]   INT            IDENTITY (1, 1) NOT NULL,
    [DesName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Designition] PRIMARY KEY CLUSTERED ([DesId] ASC)
);




