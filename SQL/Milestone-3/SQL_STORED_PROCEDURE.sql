-- Question 1
CREATE PROCEDURE Foundation.usp_InsertMovie (
	@Name VARCHAR(200)
	,@YearOfRelease INT
	,@Plot VARCHAR(500)
	,@PosterImagePath VARCHAR(500)
	,@ProducerId INT
	,@ActorIds VARCHAR(100)
	)
AS
BEGIN
	DECLARE @MovieId INT;

	BEGIN TRY
		BEGIN TRANSACTION;

		INSERT INTO Foundation.Movies (
			Name
			,YearOfRelease
			,Plot
			,PosterImagePath
			,ProducerId
			)
		VALUES (
			@Name
			,@YearOfRelease
			,@Plot
			,@PosterImagePath
			,@ProducerId
			);

		SET @MovieId = SCOPE_IDENTITY();

		INSERT INTO Foundation.Actors_Movies (
			ActorId
			,MovieId
			)
		SELECT CAST(value AS INT)
			,@MovieId
		FROM STRING_SPLIT(@ActorIds, ',');

		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH
END;

-- Question 2
CREATE PROCEDURE Foundation.usp_DeleteMovie (@Id INT)
AS
BEGIN
	DELETE
	FROM Foundation.Actors_Movies
	WHERE MovieId = @Id;

	DELETE
	FROM Foundation.Movies
	WHERE Id = @Id;
END;

-- Question 3
CREATE PROCEDURE Foundation.usp_DeleteProducer (@Id INT)
AS
BEGIN
	DELETE AM
	FROM Foundation.Actors_Movies AM
	INNER JOIN Foundation.Movies M ON AM.MovieId = M.Id
	WHERE M.ProducerId = @Id;

	DELETE
	FROM Foundation.Movies
	WHERE ProducerId = @Id;

	DELETE
	FROM Foundation.Producers
	WHERE Id = @Id;
END;

-- Question 4
CREATE PROCEDURE Foundation.usp_DeleteActor (@Id INT)
AS
BEGIN
	DELETE
	FROM Foundation.Actors_Movies
	WHERE ActorId = @Id;

	DELETE
	FROM Foundation.Actors
	WHERE Id = @Id;
END;