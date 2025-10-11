/****** Object:  StoredProcedure [dbo].[usp_insertNotes]    Script Date: 23/05/2024 16:44:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_insertNotes]
GO

/****** Object:  StoredProcedure [dbo].[usp_insertNotes]    Script Date: 23/05/2024 16:44:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_insertNotes]
@title as nvarchar(50),
@DescriptionNotes as nvarchar(max),
@DateNotes as datetime,
@CategoryNotes as int,
@ReturnCode as int output

AS
BEGIN TRY
	BEGIN TRAN
	--VARIABILI
	DECLARE @id as int = Coalesce( (select max(Notes_id) + 1 from dbo.Notes),1); --PRENDO L'ID
	declare @errormsg as nvarchar(max) = '';
	
	
	--SETTAGGIO VARIABILI
	SET @ReturnCode = -1 --<-- -1 = NOT SET;

	--se i valori sono nulli allora mando un errore
	IF (@title is null or @DescriptionNotes is null or @DateNotes is null or @CategoryNotes is null)
	BEGIN
		SET @ReturnCode = 20
		set @errormsg = 'i valori non possono essere nulli'
		RAISERROR(@errormsg, 16, 20) 
	END

	--Altrimenti inserisco
	insert into dbo.Notes(Notes_id,Title,DateNotes,DescriptionNotes,CategoryNotes)
	values(@id, @title,@DateNotes, @DescriptionNotes,@CategoryNotes)

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


