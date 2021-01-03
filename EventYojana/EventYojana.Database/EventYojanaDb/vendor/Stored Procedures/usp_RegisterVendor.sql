
CREATE PROCEDURE [vendor].[usp_RegisterVendor]
(
	@VendorName NVARCHAR(50),
	@VendorEmail NVARCHAR(50),
	@IsBranch bit,
	@Mobile NVARCHAR(15),
	@Landline NVARCHAR(15),
	@AddressLine NVARCHAR(max),
	@City NVARCHAR(50),
	@State NVARCHAR(50),
	@PinCode INT,
	@IsUserExists bit OUTPUT,
	@Success bit OUTPUT
)
AS
BEGIN
	
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE @VendorId INT;

		IF NOT EXISTS(SELECT VendorId FROM [vendor].[VendorDetails] WHERE [VendorEmail] = @VendorEmail)
		BEGIN
			SET @IsUserExists = 0;
			DECLARE @AddressId INT;

			INSERT INTO [dbo].[Address]
			   ([AddressLine]
			   ,[City]
			   ,[State]
			   ,[PinCode])
			 VALUES
				   (@AddressLine
				   ,@City
				   ,@State
				   ,@PinCode)

			SET @AddressId = @@IDENTITY

			INSERT INTO [vendor].[VendorDetails]
			   ([VendorName]
			   ,[VendorEmail]
			   ,[IsBranch]
			   ,[Mobile]
			   ,[Landline]
			   ,[AddressId]
			   ,[IsLoginByVendor])
			 VALUES
				   (@VendorName
				   ,@VendorEmail
				   ,@IsBranch
				   ,@Mobile
				   ,@Landline
				   ,@AddressId
				   ,0)

			SET @VendorId = @@IDENTITY

		END
		ELSE
		BEGIN
			SET @IsUserExists = 1;
		END

		SET @Success = 1;

		SELECT VendorId, VendorName, VendorEmail, Mobile, Landline FROM [vendor].[VendorDetails] WHERE VendorId = @VendorId

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH

	  SET @Success = 0;

      ROLLBACK TRANSACTION

	END CATCH

END