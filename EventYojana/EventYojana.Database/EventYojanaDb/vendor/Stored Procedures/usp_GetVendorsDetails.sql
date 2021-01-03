
CREATE PROCEDURE [vendor].[usp_GetVendorsDetails]
(
	@HaveLogIn BIT
)
AS 
BEGIN

	IF(@HaveLogIn = 0)
	BEGIN
		SELECT [VendorId]
			  ,[LoginId]
			  ,[VendorName]
			  ,[VendorEmail]
			  ,[IsBranch]
			  ,[Mobile]
			  ,[Landline]
			  ,VD.[AddressId]
			  ,ISNULL([AddressLine], '') + ' ' + ISNULL([City], '') + ' ' + ISNULL([State], '') + ' ' + ISNULL(CONVERT(VARCHAR(10),[PinCode]), '') AS FullAddress
			  ,[IsLoginByVendor]
		  FROM [vendor].[VendorDetails] VD
		  INNER JOIN [dbo].[Address] AD ON VD.[AddressId] = AD.[AddressId]
		  WHERE [LoginId] IS NULL
	END
	ELSE
	BEGIN
		SELECT [VendorId]
			  ,[LoginId]
			  ,[VendorName]
			  ,[VendorEmail]
			  ,[IsBranch]
			  ,[Mobile]
			  ,[Landline]
			  ,VD.[AddressId]
			  ,ISNULL([AddressLine], '') + ' ' + ISNULL([City], '') + ' ' + ISNULL([State], '') + ' ' + ISNULL(CONVERT(VARCHAR(10),[PinCode]), '') AS FullAddress
			  ,[IsLoginByVendor]
		  FROM [vendor].[VendorDetails] VD
		  INNER JOIN [dbo].[Address] AD ON VD.[AddressId] = AD.[AddressId]
	END

END