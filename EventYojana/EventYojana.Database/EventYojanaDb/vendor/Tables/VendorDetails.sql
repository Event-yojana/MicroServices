CREATE TABLE [vendor].[VendorDetails] (
    [VendorId]        INT           IDENTITY (1, 1) NOT NULL,
    [LoginId]         INT           NULL,
    [VendorName]      NVARCHAR (50) NOT NULL,
    [VendorEmail]     NVARCHAR (50) NOT NULL,
    [IsBranch]        BIT           NOT NULL,
    [Mobile]          NVARCHAR (15) NULL,
    [Landline]        NVARCHAR (15) NULL,
    [AddressId]       INT           NOT NULL,
    [IsLoginByVendor] BIT           NOT NULL,
    [CreatedDate]     DATETIME      CONSTRAINT [DF_VendorDetails_CreatedDate] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([VendorId] ASC),
    FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
    FOREIGN KEY ([LoginId]) REFERENCES [dbo].[UserLogin] ([LoginId])
);



