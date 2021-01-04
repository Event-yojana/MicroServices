
CREATE PROCEDURE [dbo].[usp_GetModuleData]
(
	@roleId INT
)
AS
BEGIN

	SELECT 
		ModuleId,
		M.ModuleName,
		IsView,
		IsAdd,
		IsEdit,
		IsDelete,
		ApplicationId
	FROM [dbo].[RoleModule] RM
	INNER JOIN [dbo].[Module] M ON RM.ModuleId = M.Id
	INNER JOIN [dbo].[Application] A ON M.ApplicationId = A.Id
	WHERE M.IsActive = 1 AND RM.RoleId = @roleId

END