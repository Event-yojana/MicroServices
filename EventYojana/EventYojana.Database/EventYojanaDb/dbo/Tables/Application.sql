CREATE TABLE [dbo].[Application] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ApplicationCode] VARCHAR (10)  NOT NULL,
    [ApplicationName] NVARCHAR (50) NULL,
    [CreatedDate]     DATETIME      CONSTRAINT [DF_Application_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED ([Id] ASC)
);

