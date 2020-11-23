CREATE TABLE [dbo].[VendorDetails] (
    [VendorId]    INT           IDENTITY (1, 1) NOT NULL,
    [LoginId]     INT           NULL,
    [VendorName]  NVARCHAR (50) NOT NULL,
    [VendorEmail] NVARCHAR (50) NOT NULL,
    [IsBranch]    BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([VendorId] ASC),
    FOREIGN KEY ([LoginId]) REFERENCES [dbo].[UserLogin] ([LoginId])
);

