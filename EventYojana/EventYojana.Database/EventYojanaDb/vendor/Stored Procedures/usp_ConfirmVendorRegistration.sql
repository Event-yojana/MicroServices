
CREATE PROCEDURE [vendor].[usp_ConfirmVendorRegistration]
(
	@VendorId INT,
	@Password NVARCHAR(MAX),
	@PasswordSalt NVARCHAR(MAX),
	@IsUserExists bit OUTPUT,
	@Success bit OUTPUT
)
AS 
BEGIN
	
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE @Username NVARCHAR(50),
				@LoginId INT;

		SELECT @Username = VendorEmail FROM [vendor].[VendorDetails] WHERE VendorId = @VendorId 
	
		IF NOT EXISTS(SELECT LoginId FROM [dbo].[UserLogin] WHERE Username = @Username)
		BEGIN

			SET @IsUserExists = 0;

			INSERT INTO [dbo].[UserLogin](UserType, Username, Password, PasswordSalt, IsVerifiedUser)
			VALUES (3, @Username, @Password, @PasswordSalt, 1)

			SET @LoginId = @@IDENTITY;

			UPDATE [vendor].[VendorDetails] SET [LoginId] = @LoginId WHERE VendorId = @VendorId;


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