
CREATE PROCEDURE [vendor].[usp_RegisterVendor]
(
	@VendorName NVARCHAR(50),
	@VendorEmail NVARCHAR(50),
	@IsBranch bit,
	@UserType INT,
	@Username NVARCHAR(50),
	@Password NVARCHAR(MAX),
	@Passwordsalt NVARCHAR(MAX)
)
AS
BEGIN
	
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE @LoginId INT;

		INSERT INTO [dbo].[UserLogin]
			   ([UserType]
			   ,[Username]
			   ,[Password]
			   ,[PasswordSalt]
			   ,[IsVerifiedUser])
		 VALUES
			   (@UserType
			   ,@Username
			   ,@Password
			   ,@Passwordsalt
			   ,0)

		SET @LoginId = @@IDENTITY

		INSERT INTO [dbo].[VendorDetails]
			   ([LoginId]
			   ,[VendorName]
			   ,[VendorEmail]
			   ,[IsBranch])
		 VALUES
			   (@LoginId
			   ,@VendorName
			   ,@VendorEmail
			   ,@IsBranch)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH

      ROLLBACK TRANSACTION [Tran1]

	END CATCH

END