-- Question 1
CREATE PROCEDURE Foundation.usp_InsertMovie
(
    @Name VARCHAR(200),
    @YearOfRelease INT,
    @Plot VARCHAR(500),
    @PosterImagePath VARCHAR(500),
    @ProducerId INT,
    @ActorIds VARCHAR(100)  
)
AS
BEGIN

    DECLARE @MovieId INT;

    INSERT INTO Foundation.Movies (MovieName, YearOfRelease, Plot, PosterImagePath, ProducerId)
    VALUES (@Name, @YearOfRelease, @Plot, @PosterImagePath, @ProducerId);

    SET @MovieId = SCOPE_IDENTITY();

    INSERT INTO Foundation.Actors_Movies (ActorId, MovieId)
    SELECT CAST(value AS INT), @MovieId
    FROM STRING_SPLIT(@ActorIds, ',');

END;

-- Question 2

CREATE PROCEDURE Foundation.usp_DeleteMovie
(
    @MovieId INT
)
AS
BEGIN

    DELETE FROM Foundation.Actors_Movies
    WHERE MovieId = @MovieId;

    DELETE FROM Foundation.Movies
    WHERE Id = @MovieId;

END;

-- Question 3

CREATE PROCEDURE Foundation.usp_DeleteProducer
(
    @ProducerId INT
)
AS
BEGIN

    DELETE AM
    FROM Foundation.Actors_Movies AM
    INNER JOIN Foundation.Movies M
        ON AM.MovieId = M.Id
    WHERE M.ProducerId = @ProducerId;

    DELETE FROM Foundation.Movies
    WHERE ProducerId = @ProducerId;

    DELETE FROM Foundation.Producers
    WHERE Id = @ProducerId;

END;

-- Question 4

CREATE PROCEDURE Foundation.usp_DeleteActor
(
    @ActorId INT
)
AS
BEGIN

    DELETE FROM Foundation.Actors_Movies
    WHERE ActorId = @ActorId;

    DELETE FROM Foundation.Actors
    WHERE Id = @ActorId;

END;
