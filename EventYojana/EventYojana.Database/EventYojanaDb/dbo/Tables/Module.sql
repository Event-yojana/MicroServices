CREATE TABLE [dbo].[Module] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [ModuleName]    VARCHAR (50) NOT NULL,
    [ApplicationId] INT          NOT NULL,
    [IsActive]      BIT          CONSTRAINT [DF_Module_IsActive] DEFAULT ((0)) NOT NULL,
    [CreatedDate]   DATETIME     CONSTRAINT [DF_Module_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Module_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Application] ([Id])
);

