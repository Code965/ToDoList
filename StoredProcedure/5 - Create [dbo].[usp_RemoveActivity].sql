/****** Object:  StoredProcedure [dbo].[usp_RemoveActivity]    Script Date: 04/12/2023 13:51:17 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_RemoveActivity]
GO

/****** Object:  StoredProcedure [dbo].[usp_RemoveActivity]    Script Date: 04/12/2023 13:51:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_RemoveActivity]

@ActivityId as int,
@ReturnCode as int output 

AS
begin try
begin tran
	declare @errormsg as nvarchar(max) = ''
	SET @ReturnCode = -1 --<-- -1 = NOT SET

	
	delete dbo.Activity where activity_id = @ActivityId

commit tran
	SET @ReturnCode = 0 --<-- 0 IS OK!
end try
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
