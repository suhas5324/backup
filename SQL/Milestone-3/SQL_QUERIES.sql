-- 1.Write a query to get the age of the Actors in Days(Number of days).
SELECT ActorName
	,CASE 
		WHEN DateOfBirth IS NOT NULL
			THEN DATEDIFF(DAY, DateOfBirth, GETDATE())
		ELSE NULL
		END AS AgeInTotalDays
FROM Foundation.Actors

-- 2.Write a query to get the list of Actors who have worked with a given producer X.
SELECT DISTINCT A.ActorName
FROM Foundation.Actors A
JOIN Foundation.Actors_Movies AM ON A.Id = AM.ActorId
JOIN Foundation.Movies M ON AM.MovieId = M.Id
JOIN Foundation.Producers P ON M.ProducerId = P.Id
WHERE P.ProducerName = 'X'

-- 3.Write a query to get the list of actors who have acted together in two or more movies.
SELECT A1.ActorName AS Actor1
	,A2.ActorName AS Actor2
	,COUNT(*) AS MoviesTogether
FROM Foundation.Actors_Movies AM1
JOIN Foundation.Actors_Movies AM2 ON AM1.MovieId = AM2.MovieId
	AND AM1.ActorId < AM2.ActorId
JOIN Foundation.Actors A1 ON AM1.ActorId = A1.Id
JOIN Foundation.Actors A2 ON AM2.ActorId = A2.Id
GROUP BY A1.ActorName
	,A2.ActorName
HAVING COUNT(*) >= 2

-- 4.Write a query to get the youngest actor.
SELECT *
FROM Foundation.Actors
WHERE DateOfBirth = (
		SELECT MAX(DateOfBirth)
		FROM Foundation.Actors
		);

-- 5.Write a query to get the actors who have never worked together.
SELECT A1.ActorName AS Actor1
	,A2.ActorName AS Actor2
FROM Foundation.Actors A1
JOIN Foundation.Actors A2 ON A1.Id < A2.Id
WHERE NOT EXISTS (
		SELECT 1
		FROM Foundation.Actors_Movies AM1
		JOIN Foundation.Actors_Movies AM2 ON AM1.MovieId = AM2.MovieId
		WHERE AM1.ActorId = A1.Id
			AND AM2.ActorId = A2.Id
		)

-- 6.Write a query to get the number of movies in each language.
SELECT MovieLanguage
	,COUNT(*) AS MovieCountByLanguage
FROM Foundation.Movies
GROUP BY MovieLanguage

-- 7.Write a query to get me the total profit of all the movies in each language separately.
SELECT MovieLanguage
	,SUM(Profit) AS TotalProfitByLanguage
FROM Foundation.Movies
GROUP BY MovieLanguage

-- 8.Write a query to get the total profit of movies which have actor X in each language.
SELECT M.MovieLanguage
	,SUM(M.Profit) AS TotalProfit
FROM Foundation.Movies M
JOIN Foundation.Actors_Movies AM ON M.Id = AM.MovieId
JOIN Foundation.Actors A ON AM.ActorId = A.Id
WHERE A.ActorName = 'X'
GROUP BY M.MovieLanguage

-- 9.Write a query to get the Total profit by year of release and language
SELECT YearOfRelease
	,MovieLanguage
	,SUM(Profit) AS TotalProfitByYearAndLanguage
FROM Foundation.Movies
GROUP BY YearOfRelease
	,MovieLanguage

-- 10.Write a query to get number of movies in each language produced by each producer
SELECT P.ProducerName
	,M.MovieLanguage
	,COUNT(*) AS MovieCount
FROM Foundation.Movies M
JOIN Foundation.Producers P ON M.ProducerId = P.Id
GROUP BY P.ProducerName
	,M.MovieLanguage