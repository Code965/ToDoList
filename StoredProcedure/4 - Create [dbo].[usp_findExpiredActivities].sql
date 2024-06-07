/****** Object:  StoredProcedure [dbo].[usp_findExpiredActivities]    Script Date: 28/05/2024 10:58:26 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_findExpiredActivities]
GO

/****** Object:  StoredProcedure [dbo].[usp_findExpiredActivities]    Script Date: 28/05/2024 10:58:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_findExpiredActivities]
@today as Date,
@ReturnCode as int output
AS
BEGIN TRY
	BEGIN TRAN

	--VARIABILI
	declare @errormsg as nvarchar(max) = '';
	
	--Data di oggi
	declare @dataToday date;
	SET @dataToday = CONVERT(DATE,@today);
	
	--Data di ieri
	declare @dataYesterday date;
	SET @dataYesterday = DATEADD(day,-1,GETDATE());

	
	select *
	from dbo.Activity
	where dateActivity < @dataToday;

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


