/****** Object:  StoredProcedure [dbo].[usp_findUser]    Script Date: 24/05/2024 14:52:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_findUser]
GO

/****** Object:  StoredProcedure [dbo].[usp_findUser]    Script Date: 24/05/2024 14:52:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_findUser]
@username as nvarchar(max)
AS
BEGIN TRY
	BEGIN TRAN

	--VARIABILI
	declare @errormsg as nvarchar(max) = '';

	IF (@username is null)
	BEGIN
		set @errormsg = 'i valori non possono essere nulli'
		RAISERROR(@errormsg, 16, 20) 
	END

	select *
	from dbo.Users
	where name = @username

	COMMIT TRAN
	
END TRY

BEGIN CATCH
	IF(@@TRANCOUNT > 0)
	BEGIN
		ROLLBACK TRAN
	END

	declare @state as int = ERROR_STATE()
	declare @severity as int = ERROR_SEVERITY()
	declare @errmessage as nvarchar(max) = ERROR_MESSAGE()
	RAISERROR(@errmessage, @severity, @state)
END CATCH
GO


