
CREATE PROCEDURE vendor.usp_GetVendorsDetails
(
	@HaveLogIn BIT
)
AS 
BEGIN

	SELECT [VendorId]
      ,[LoginId]
      ,[VendorName]
      ,[VendorEmail]
      ,[IsBranch]
      ,[Mobile]
      ,[Landline]
      ,VD.[AddressId]
	  ,ISNULL([AddressLine], '') + ' ' + ISNULL([City], '') + ' ' + ISNULL([State], '') + ' ' + ISNULL([PinCode], '') AS FullAddress
      ,[IsLoginByVendor]
  FROM [vendor].[VendorDetails] VD
  INNER JOIN [dbo].[Address] AD ON VD.[AddressId] = AD.[AddressId]
  WHERE [LoginId] = (CASE WHEN @HaveLogIn = 0 THEN NULL ELSE [LoginId] END)

END