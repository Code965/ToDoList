/****** Object:  StoredProcedure [dbo].[usp_insertUser]    Script Date: 03/04/2024 09:00:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_insertUser]
GO

/****** Object:  StoredProcedure [dbo].[usp_insertUser]    Script Date: 03/04/2024 09:00:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_insertUser]
@name as nvarchar(50),
@surname as nvarchar(50),
@email as nvarchar(256),
@password as nvarchar(50),
@dateBirth as datetime,
@ReturnCode as int output

AS
BEGIN TRY
	BEGIN TRAN
	--VARIABILI
	DECLARE @id as int = Coalesce( (select max(id_user) + 1 from dbo.Users),1); --PRENDO L'ID
	declare @errormsg as nvarchar(max) = '';
	
	
	--SETTAGGIO VARIABILI
	SET @ReturnCode = -1 --<-- -1 = NOT SET;

	--se i valori sono nulli allora mando un errore
	IF (@name is null or @surname is null or @email is null or @password is null or @dateBirth is null)
	BEGIN
		SET @ReturnCode = 20
		set @errormsg = 'i valori non possono essere nulli'
		RAISERROR(@errormsg, 16, 20) 
	END

	--Altrimenti inserisco
	insert into dbo.Users(id_user,name,surname,email,password,dateBirth)
	values(@id, @name, @surname, @email,@password,@dateBirth)

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


