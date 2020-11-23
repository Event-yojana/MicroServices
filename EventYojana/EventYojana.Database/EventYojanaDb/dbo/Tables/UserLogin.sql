CREATE TABLE [dbo].[UserLogin] (
    [LoginId]        INT            IDENTITY (1, 1) NOT NULL,
    [UserType]       INT            NOT NULL,
    [Username]       NVARCHAR (50)  NOT NULL,
    [Password]       NVARCHAR (MAX) NOT NULL,
    [PasswordSalt]   NVARCHAR (MAX) NOT NULL,
    [IsVerifiedUser] BIT            NULL,
    PRIMARY KEY CLUSTERED ([LoginId] ASC),
    FOREIGN KEY ([UserType]) REFERENCES [dbo].[UserRoles] ([RoleId])
);

