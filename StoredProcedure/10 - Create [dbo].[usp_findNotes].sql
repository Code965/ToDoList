/****** Object:  StoredProcedure [dbo].[usp_findNotes]    Script Date: 24/05/2024 14:52:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_findNotes]
GO

/****** Object:  StoredProcedure [dbo].[usp_findNotes]    Script Date: 24/05/2024 14:52:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_findNotes]
@ReturnCode as int output
AS
BEGIN TRY
	BEGIN TRAN

	--VARIABILI
	declare @errormsg as nvarchar(max) = '';

	--Altrimenti inserisco
	select *
	from dbo.Notes
	

	COMMIT TRAN
	
	SET @ReturnCode = 0 --<-- 0 IS OK!
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


