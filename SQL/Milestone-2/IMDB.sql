CREATE DATABASE IMDB;
GO

CREATE SCHEMA FOUNDATION;
GO

CREATE TABLE FOUNDATION.ACTORS(
    Id INT IDENTITY(1,1),
    ActorName VARCHAR(50) NOT NULL,
    Sex VARCHAR(20),
    DateOfBirth DATE,
    Bio VARCHAR(100),

    CONSTRAINT PK_Foundation_Actors_Id
    PRIMARY KEY (Id),

    CONSTRAINT CK_Foundation_Actors_DateOfBirth 
        CHECK (DateOfBirth < GETDATE())
);

CREATE TABLE FOUNDATION.PRODUCERS(
    Id INT IDENTITY(1,1),
    ProducerName VARCHAR(50) NOT NULL,
    Sex VARCHAR(20),
    DateOfBirth DATE,
    Bio VARCHAR(100),

    CONSTRAINT PK_Foundation_Producers_Id
    PRIMARY KEY (Id),

    CONSTRAINT CK_Foundation_Producers_DateOfBirth 
        CHECK (DateOfBirth < GETDATE())
);

CREATE TABLE FOUNDATION.MOVIES(
    Id INT IDENTITY(1,1),
    MovieName VARCHAR(100) NOT NULL,
    YearOfRelease INT NOT NULL,
    Plot VARCHAR(500),
    PosterImagePath VARCHAR(500),
    ProducerId INT NOT NULL,

    CONSTRAINT PK_Foundation_Movies_Id 
        PRIMARY KEY (Id),

    CONSTRAINT FK_Foundation_Movies_Producer_Id 
        FOREIGN KEY (ProducerId) 
        REFERENCES FOUNDATION.PRODUCERS(Id),

    CONSTRAINT CK_Foundation_Movies_YearOfRelease 
        CHECK (YearOfRelease <= YEAR(GETDATE()))
);

CREATE TABLE FOUNDATION.ACTORS_MOVIE(
    Id INT IDENTITY(1,1),
    ActorId INT NOT NULL,
    MovieId INT NOT NULL,

    CONSTRAINT PK_Foundation_Actors_Movie_Id
    PRIMARY KEY (Id),

    CONSTRAINT FK_Foundation_Actors_Movie_ActorId
    FOREIGN KEY (ActorId) 
    REFERENCES FOUNDATION.ACTORS (Id),

    CONSTRAINT FK_Foundation_Actors_Movie_MovieId
    FOREIGN KEY (MovieId)
    REFERENCES FOUNDATION.MOVIES (Id),

    CONSTRAINT UQ_Foundation_Actors_Movie_ActorId_Movie_Id
    UNIQUE (ActorId,MovieId)
    );

ALTER TABLE Foundation.Producers
ADD CreatedAt DATETIME,
    UpdatedAt DATETIME;

ALTER TABLE Foundation.Actors
ADD CreatedAt DATETIME,
	UpdatedAt DATETIME;

ALTER TABLE Foundation.Movies
ADD CreatedAt DATETIME,
	UpdatedAt DATETIME;

ALTER TABLE Foundation.Actors_Movie
ADD CreatedAt DATETIME,
	UpdatedAt DATETIME;


ALTER TABLE Foundation.Producers
ADD CONSTRAINT DF_Foundation_Producers_CreatedAt
DEFAULT GETDATE() FOR CreatedAt;

ALTER TABLE Foundation.Actors
ADD CONSTRAINT DF_Foundation_Actors_CreatedAt
DEFAULT GETDATE() FOR CreatedAt;

ALTER TABLE Foundation.Movies
ADD CONSTRAINT DF_Foundation_Movies_CreatedAt
DEFAULT GETDATE() FOR CreatedAt;

ALTER TABLE Foundation.Actors_Movie
ADD CONSTRAINT DF_Foundation_ActorsMovie_CreatedAt
DEFAULT GETDATE() FOR CreatedAt;

ALTER TABLE Foundation.Movies
ADD MovieLanguage VARCHAR(50),
	Profit INT;

INSERT INTO Foundation.Producers
(
	ProducerName,
	DateOfBirth,
	Bio
)
VALUES
('Karan Johar','1972-05-25','Bollywood producer'),
('Christopher Nolan','1970-07-30','Hollywood producer');


INSERT INTO Foundation.Actors
(
	ActorName,
	DateOfBirth,
	Bio
)
VALUES
('Actor A','1980-01-01','Bio A'),
('Actor B','1985-02-02','Bio B'),
('Actor C','1990-03-03','Bio C'),
('Actor D','1992-04-04','Bio D');


INSERT INTO Foundation.Movies
(
	MovieName,
	YearOfRelease,
	Plot,
	PosterImagePath,
	ProducerId,
	MovieLanguage,
	Profit
)
VALUES
('Movie 1',2020,'Plot 1','url1',1,'Kannada',100),
('Movie 2',2021,'Plot 2','url2',1,'Kannada',150),
('Movie 3',2022,'Plot 3','url3',1,'English',200),
('Movie 4',2023,'Plot 4','url4',1,'English',250),
('Movie 5',2019,'Plot 5','url5',2,'English',300);


INSERT INTO Foundation.Actors_Movie (ActorId, MovieId)
VALUES
(1,1),
(2,1),
(3,1);

INSERT INTO Foundation.Actors_Movie (ActorId, MovieId)
VALUES
(1,2),
(2,2);

INSERT INTO Foundation.Actors_Movie (ActorId, MovieId)
VALUES
(3,3),
(4,4),
(1,5);