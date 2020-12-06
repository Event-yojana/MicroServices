CREATE TABLE [dbo].[EmailLogs] (
    [EmailLogId]       INT            IDENTITY (1, 1) NOT NULL,
    [FromEmailAddress] NVARCHAR (50)  NOT NULL,
    [ToEmailAddress]   NVARCHAR (50)  NOT NULL,
    [Subject]          NVARCHAR (MAX) NULL,
    [Body]             NVARCHAR (MAX) NULL,
    [IsProduction]     BIT            NOT NULL,
    [IsSend]           BIT            NOT NULL,
    [ApplicationId]    INT            NOT NULL,
    [FromUserType]     NVARCHAR (25)  NOT NULL,
    [FromUserId]       INT            NULL,
    [ToUserType]       NVARCHAR (25)  NOT NULL,
    [ToUserId]         INT            NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    CONSTRAINT [PK_EmailLogs] PRIMARY KEY CLUSTERED ([EmailLogId] ASC)
);

