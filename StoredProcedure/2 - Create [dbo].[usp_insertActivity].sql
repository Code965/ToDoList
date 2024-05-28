/****** Object:  StoredProcedure [dbo].[usp_insertActivity]    Script Date: 23/05/2024 16:44:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_insertActivity]
GO

/****** Object:  StoredProcedure [dbo].[usp_insertActivity]    Script Date: 23/05/2024 16:44:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_insertActivity]
@name as nvarchar(50),
@description as nvarchar(50),
@date as datetime,
@priority as int,
@category as int,
@ReturnCode as int output

AS
BEGIN TRY
	BEGIN TRAN
	--VARIABILI
	DECLARE @id as int = Coalesce( (select max(activity_id) + 1 from dbo.Activity),1); --PRENDO L'ID
	declare @errormsg as nvarchar(max) = '';
	
	
	--SETTAGGIO VARIABILI
	SET @ReturnCode = -1 --<-- -1 = NOT SET;

	--se i valori sono nulli allora mando un errore
	IF (@name is null or @description is null or @date is null or @priority is null or @category is null)
	BEGIN
		SET @ReturnCode = 20
		set @errormsg = 'i valori non possono essere nulli'
		RAISERROR(@errormsg, 16, 20) 
	END

	--Altrimenti inserisco
	insert into dbo.Activity(activity_id,name,description,dateActivity,priority,category)
	values(@id, @name, @description,@date,@priority,@category)

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


