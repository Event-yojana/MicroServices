CREATE TABLE [dbo].[UserRoles] (
    [RoleId]          INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]        NVARCHAR (25) NULL,
    [RoleDescription] NVARCHAR (50) NULL,
    [CreatedDate]     DATETIME      CONSTRAINT [DF_UserRoles_CreatedDate] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);



