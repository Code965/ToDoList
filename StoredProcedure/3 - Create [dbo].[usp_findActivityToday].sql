/****** Object:  StoredProcedure [dbo].[usp_findActivityToday]    Script Date: 24/05/2024 14:52:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_findActivityToday]
GO

/****** Object:  StoredProcedure [dbo].[usp_findActivityToday]    Script Date: 24/05/2024 14:52:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_findActivityToday]
@today as Date,
@ReturnCode as int output
AS
BEGIN TRY
	BEGIN TRAN

	--VARIABILI
	declare @errormsg as nvarchar(max) = '';

	--Altrimenti inserisco
	select *
	from dbo.Activity
	where CONVERT(DATE, dateActivity) = CONVERT(DATE,@today)
	ORDER BY activity_id ASC;

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


