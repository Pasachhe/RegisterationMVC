CREATE TABLE [dbo].[Employee] (
    [EmployeeId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserId]         INT  NULL,
    [DepartmentId] INT           NULL,
    [Salary]      VARCHAR (150) NULL,
    [IsDeleted]    BIT           NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]), 
    CONSTRAINT [FK_Employee_SiteUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SiteUser] ([UserId])
);
