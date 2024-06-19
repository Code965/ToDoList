/****** Object:  StoredProcedure [dbo].[usp_UpdateActivity]    Script Date: 30/11/2023 08:58:30 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_UpdateActivity]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateActivity]    Script Date: 30/11/2023 08:58:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_UpdateActivity]
@activity_id as int,
@name as nvarchar(max),
@description as nvarchar(max),
@date as datetime,
@priority as int,
@category as int,
@ReturnCode as int output 
AS
SET NOCOUNT ON
BEGIN TRY
	--BEGIN TRAN
	declare @errormsg as nvarchar(max) = ''
	SET @ReturnCode = -1 --<-- -1 = NOT SET

	
	

	--SECONDO CONTROLLO
	IF (@activity_id is null or @name is null or @description is null or @date is null or @priority is null or @category is null)
	BEGIN
		SET @ReturnCode = 20
		set @errormsg = 'I valori non possono essere nulli'
		RAISERROR(@errormsg, 16, 20) 
	END

	update dbo.Activity set [name] = @name,  [description] = @description, [dateActivity] = @date, [priority] = @priority, [category]= @category where activity_id = @activity_id;
	
	
	SET @ReturnCode = 0 --<-- 0 IS OK!
END TRY

BEGIN CATCH
	IF(@@TRANCOUNT > 0)
	BEGIN
		ROLLBACK TRAN
	END
	--Se ReturnCode = -1 vuol dire che ha passato tutti i controlli formali ma c'è stata una eccezione di SQL e quindi la rilancio
	IF(@ReturnCode = -1)
	BEGIN
		declare @state as int = ERROR_STATE()
		declare @severity as int = ERROR_SEVERITY()
		declare @message as nvarchar(max) = ERROR_MESSAGE()
		RAISERROR(@message, @severity, @state)
	END
END CATCH



GO

