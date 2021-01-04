CREATE TABLE [dbo].[Address] (
    [AddressId]   INT            IDENTITY (1, 1) NOT NULL,
    [AddressLine] NVARCHAR (MAX) NOT NULL,
    [City]        NVARCHAR (50)  NOT NULL,
    [State]       NVARCHAR (50)  NOT NULL,
    [PinCode]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([AddressId] ASC)
);

