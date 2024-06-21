/****** Object:  StoredProcedure [dbo].[usp_filterActivity_ExpiredActivities]    Script Date: 28/05/2024 10:58:26 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_filterActivity_ExpiredActivities]
GO

/****** Object:  StoredProcedure [dbo].[usp_filterActivity_ExpiredActivities]    Script Date: 28/05/2024 10:58:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_filterActivity_ExpiredActivities]
@string_filter as nvarchar(max)
AS
BEGIN TRY
	BEGIN TRAN

	--VARIABILI
	declare @errormsg as nvarchar(max) = '';
	
	IF(@string_filter is null)
	BEGIN
		set @errormsg = 'I valori non possono essere nulli'
		RAISERROR(@errormsg, 16, 20) 
	END

	select *
	from dbo.Activity
	where name LIKE '%'+ @string_filter + '%' and  CAST(dateActivity as DATE) < CAST(GETDATE() AS DATE);

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


