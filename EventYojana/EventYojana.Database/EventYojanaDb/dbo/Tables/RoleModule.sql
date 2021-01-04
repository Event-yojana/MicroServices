CREATE TABLE [dbo].[RoleModule] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [RoleId]      INT      NOT NULL,
    [ModuleId]    INT      NOT NULL,
    [IsView]      BIT      CONSTRAINT [DF_RoleModule_IsView] DEFAULT ((0)) NOT NULL,
    [IsAdd]       BIT      CONSTRAINT [DF_RoleModule_IsAdd] DEFAULT ((0)) NOT NULL,
    [IsEdit]      BIT      CONSTRAINT [DF_RoleModule_IsEdit] DEFAULT ((0)) NOT NULL,
    [IsDelete]    BIT      CONSTRAINT [DF_RoleModule_IsDelete] DEFAULT ((0)) NOT NULL,
    [CreatedDate] DATETIME CONSTRAINT [DF_RoleModule_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_RoleModule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleModule_Module] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module] ([Id]),
    CONSTRAINT [FK_RoleModule_UserRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([RoleId]),
    CONSTRAINT [unq_RoleModule_RoleId_ModuleId] UNIQUE NONCLUSTERED ([RoleId] ASC, [ModuleId] ASC)
);

